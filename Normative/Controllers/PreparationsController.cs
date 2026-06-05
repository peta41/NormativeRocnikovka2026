using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Normative.Data;
using Normative.Models;
using System.Globalization;
using Normative.Models.Table;
using Normative.Models.Screen;
using X.PagedList;
using X.PagedList.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Normative.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PreparationsController : Controller
    {
        private readonly NormativeContext _context;

        public PreparationsController(NormativeContext context)
        {
            _context = context;
        }

        //GET: Preparations
        public IActionResult Index()
        {
            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "Settings",
                        Action = "Index",
                        Title = "Back",
                        Icon = "icon-reply",
                        Visible = true
                    },
                    new ToolBarActionLink
                    {
                        Controller = "Preparations",
                        Action = "Create",
                        Title = "New",
                        Icon = "icon-plus",
                        Visible = true
                    }
                };

            // add link(s) to toolbar
            ToolBar tool = new()
            {
                Action = ListAction,
            };

            PreparationModel model = new()
            {
                PreparationsList = _context.V_Preparation.ToPagedList(),
                ToolBar = tool
            };

            return View(model);
        }


        //GET: Preparations
        public async Task<IActionResult> Create()
        {
            PreparationModel model = await GetDetailAsync(0);

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "Preparations",
                        Action = "Index",
                        Title = "Back",
                        Icon = "icon-reply",
                        Visible = true
                    },
                    new ToolBarActionLink
                    {
                        //Controller = "Preparations",
                        //Action = "Create",
                        JavaScript = "Save()",
                        Title = "Save",
                        Icon = "icon-floppy",
                        Visible = true
                    }
                };

            // add link(s) to toolbar
            ToolBar tool = new()
            {
                Action = ListAction,
            };

            model.ToolBar = tool;
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(PreparationSaveModel model)
        {
            if (ModelState.IsValid)
            {
                Preparation preparation = new() {

                    ProductSize = _context.ProductSize.FirstOrDefault(f => f.ProductSizeId == model.ProductSize),
                    PreparationType = _context.PreparationType.FirstOrDefault(f => f.Id == model.PreparationType),

                    Welder = decimal.Parse(model.Welder, CultureInfo.InvariantCulture),
                    Fitter = decimal.Parse(model.Fitter, CultureInfo.InvariantCulture),
                    IsDeleted = false,
                };

                _context.Add(preparation);
                _context.SaveChanges();

            
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }



        //funkce na ziskani details nasledne pouzivano pro: details, edit, delete.
        private async Task<PreparationModel> GetDetailAsync(int? id)
        {

            var preparation = await _context.Preparations.Include(i=> i.ProductSize).Include(i => i.PreparationType).FirstOrDefaultAsync(f=> f.Id == id); //Preparations not Preparation
            var productSizes = await _context.ProductSize.ToListAsync();
            var preparationsType = await _context.PreparationType.ToListAsync();
            PreparationModel model = new()
            {
                Preparation = preparation,
                ProductSizes = productSizes,
                PreparationsType = preparationsType
            };
            return model;
        }


        // GET: Operations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PreparationModel model = await GetDetailAsync(id);

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "Preparations",
                        Action = "Index",
                        Title = "Back",
                        Icon = "icon-reply",
                        Visible = true
                    },
                    new ToolBarActionLink
                    {
                        JavaScript = "Save()",
                        Title = "Save",
                        Icon = "icon-floppy",
                        Visible = true
                    }
                };

            // add link(s) to toolbar
            ToolBar tool = new()
            {
                Action = ListAction,
            };


            model.ToolBar = tool;

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Operations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OperationId,ProductLineId,ProductTypeId,OperationNumber,OperationDescription,WorkCenter")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    operation.OperationId = id;
                    _context.Update(operation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationExists(operation.OperationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(operation);


        }


        public IActionResult Details(int id)
        {

            PreparationModel model = new();
            Preparation pr = _context.Preparations.FirstOrDefault(x => x.Id == id);
            List<ProductSize> size = _context.ProductSize.Skip(7).ToList();
            List<PreparationType> type = _context.PreparationType.ToList();
            IPagedList<V_Preparation> prep = _context.V_Preparation.ToPagedList();

            model.Preparation = pr;
            model.PreparationsType = type;
            model.ProductSizes = size;
            model.PreparationsList = prep;

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "Preparations",
                        Action = "Index",
                        Title = "Back",
                        Icon = "icon-reply",
                        Visible = true
                    },
                    new ToolBarActionLink
                    {
                        Controller = "Preparations",
                        Action = "Edit",
                        Title = "Edit",
                        Icon = "icon-pencil",
                        Visible = true,
                        Parameter = id.ToString()
                    }
                };

            // add link(s) to toolbar
            ToolBar tool = new()
            {
                Action = ListAction,
            };


            model.ToolBar = tool;

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        [HttpPost]
        public IActionResult Save(PreparationSaveModel model)
        {
            if (ModelState.IsValid)
            {
                Preparation preparation = _context.Preparations.Include(p => p.ProductSize).Include(p => p.PreparationType).FirstOrDefault(f => f.Id == model.Id);


                if (preparation != null)
                {
                    preparation.ProductSize = _context.ProductSize.FirstOrDefault(f => f.ProductSizeId == model.ProductSize);
                    preparation.PreparationType = _context.PreparationType.FirstOrDefault(f => f.Id == model.PreparationType);
                    preparation.Welder = decimal.Parse(model.Welder, CultureInfo.InvariantCulture);
                    preparation.Fitter = decimal.Parse(model.Fitter, CultureInfo.InvariantCulture);

                    //_context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Detail", model.Id);
                }

            }


            return RedirectToAction("Detail", model.Id);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                Preparation preparation = _context.Preparations.Include(p => p.ProductSize).Include(p => p.PreparationType).FirstOrDefault(f => f.Id == id);


                if (preparation != null)
                {
                    preparation.IsDeleted = true;

                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Detail", id);
                }

            }


            return RedirectToAction("Detail", id);

        }

        //add
        private bool OperationExists(int id)
        {
            return _context.Operation.Any(e => e.OperationId == id);
        }
        //add end


        // GET: ProductSizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PreparationModel model = await GetDetailAsync(id);

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "Preparations",
                        Action = "Index",
                        Title = "Back",
                        Icon = "icon-reply",
                        Visible = true
                    },
                    new ToolBarActionLink
                    {
                        JavaScript = "Save()",
                        Title = "Delete",
                        Icon = "icon-trash",
                        Visible = true
                    }
                };

            // add link(s) to toolbar
            ToolBar tool = new()
            {
                Action = ListAction,
            };


            model.ToolBar = tool;

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

    }
}