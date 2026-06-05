using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Normative.Data;
using Normative.Models.Home;
using Normative.Models.Screen;
using Normative.Models.Table;
using X.PagedList;
using X.PagedList.Extensions;

namespace Normative.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class OperationsController : Controller
    {
        private readonly NormativeContext _context;
        //private readonly SharedService _sharedService;

        public OperationsController(NormativeContext context/*, SharedService sharedService*/)
        {
            _context = context;
            //_sharedService = sharedService;
        }

        // GET: Operations
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
                        Controller = "Operations",
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

            OperationModel model = new()
            {
                Operations = _context.Operation.Where(w => w.IsDeleted == false)
                    .Include(i=>i.ProductLine).Include(i=>i.ProductType).ToPagedList(),
                ToolBar = tool
            };

            return View(model);


            //IPagedList<Operation> operation = _context.Operation.Where(w => w.IsDeleted == false)
            //    .Include(o => o.ProductLine).Where(w => w.ProductLine.IsDeleted == false)
            //    .Include(o => o.ProductType).Where(w => w.ProductType.IsDeleted == false).ToPagedList();

            //OperationModel model = new()
            //{
            //    Operations = operation,
            //    Navigation = await _sharedService.NavigationAsync(),
            //    ToolBar = tool,
            //};

            //return View(model);
        }


        //funkce na ziskani details nasledne pouzivano pro: details, edit, delete.
        private async Task<OperationModel> GetDetailAsync(int? id)
        {
            var operation = await _context.Operation.FindAsync(id);
            OperationModel model = new()
            {
                Operation = operation
            };
            return model;
        }

        // GET: Operations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OperationModel model = await GetDetailAsync(id);

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "Operations",
                        Action = "Index",
                        Title = "Back",
                        Icon = "icon-reply",
                        Visible = true
                    },
                    new ToolBarActionLink
                    {
                        Controller = "Operations",
                        Action = "Edit",
                        Title = "Edit",
                        Icon = "icon-pencil",
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

            //Operation operation = await _context.Operation.Where(w => w.OperationId == id)
            //.Include(o => o.ProductLine)
            //.Include(o => o.ProductType)
            //.FirstOrDefaultAsync();

            //if (operation == null)
            //{
            //    return NotFound();
            //}

            //SettingModel model = new()
            //{
            //    Operation = operation,
            //};

            //return View(model);
        }

        // GET: Operations/Create
        public IActionResult Create()
        {
            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "Operations",
                        Action = "Index",
                        Title = "Back",
                        Icon = "icon-reply",
                        Visible = true
                    },
                    new ToolBarActionLink
                    {
                        //Controller = "ProductType",
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

            OperationModel model = new() { ToolBar = tool };
            return View(model);

            //ViewData["ProductLineId"] = new SelectList(_context.ProductLine, "ProductLineId", "ProductLineId");
            //ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "ProductTypeId");
            //return View();
        }

        // POST: Operations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OperationId,ProductLineId,ProductTypeId,OperationNumber,OperationDescription,WorkCenter")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(operation);

            //if (ModelState.IsValid)
            //{
            //    _context.Add(operation);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(IndexAsync));
            //}
            //ViewData["ProductLineId"] = new SelectList(_context.ProductLine, "ProductLineId", "ProductLineId", operation.ProductLineId);
            //ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "ProductTypeId", operation.ProductTypeId);
            //return View(operation);
        }

        // GET: Operations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OperationModel model = await GetDetailAsync(id);

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "Operations",
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

            //Operation operation = await _context.Operation.Where(w => w.OperationId == id)
            //.Include(o => o.ProductLine)
            //.Include(o => o.ProductType)
            //.FirstOrDefaultAsync();

            //List<ProductLine> line = await _context.ProductLine.ToListAsync();
            //List<ProductType> type = await _context.ProductType.ToListAsync();

            //SettingModel model = new()
            //{
            //    Operation = operation,
            //    ProductLines = line,
            //    ProductTypes = type,
            //};

            //return View(model);
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

            //if (id != operation.OperationId)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(operation);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!OperationExists(operation.OperationId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(IndexAsync));
            //}
            //ViewData["ProductLineId"] = new SelectList(_context.ProductLine, "ProductLineId", "ProductLineId", operation.ProductLineId);
            //ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "ProductTypeId", operation.ProductTypeId);
            //return View(operation);
        }

        // GET: Operations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OperationModel model = await GetDetailAsync(id);

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "Operations",
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

            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var operation = await _context.Operation
            //    .Include(o => o.ProductLine)
            //    .Include(o => o.ProductType)
            //    .FirstOrDefaultAsync(m => m.OperationId == id);
            //if (operation == null)
            //{
            //    return NotFound();
            //}

            //return View(model);
        }

        // POST: Operations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var operation = await _context.Operation.FindAsync(id);
            if (operation != null)
            {
                //_context.Operation.Remove(operation);
                operation.IsDeleted = true;
                _context.Operation.Update(operation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index /*IndexAsync*/));
        }

        private bool OperationExists(int id)
        {
            return _context.Operation.Any(e => e.OperationId == id);
        }
    }
}
