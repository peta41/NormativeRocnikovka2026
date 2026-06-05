using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Normative.Data;
using Normative.Models.Screen;
using Normative.Models.Table;
using X.PagedList;
using X.PagedList.Extensions;

namespace Normative.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OperationStepsController : Controller
    {
        private readonly NormativeContext _context;

        public OperationStepsController(NormativeContext context)
        {
            _context = context;
        }

        // GET: OperationSteps
        public IActionResult Index(int page = 1)
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
                        Controller = "OperationSteps",
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

            OperationStepModel model = new()
            {
                OperationSteps = _context.OperationStep.Where(w => w.IsDeleted == false).ToPagedList(page, 30),
                ToolBar = tool
            };

            return View(model);
        }


        //funkce na ziskani details nasledne pouzivano pro: details, edit, delete.
        private async Task<OperationStepModel> GetDetailAsync(int? id)
        {
            var operationStep = await _context.OperationStep.FindAsync(id);
            OperationStepModel model = new()
            {
                OperationStep = operationStep
            };
            return model;
        }

        // GET: OperationSteps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OperationStepModel model = await GetDetailAsync(id);

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
            {
                new ToolBarActionLink
                    {
                        Controller = "OperationSteps",
                        Action = "Index",
                        Title = "Back",
                        Icon = "icon-reply",
                        Visible = true
                    },
                new ToolBarActionLink
                    {
                        Controller = "OperationSteps",
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


            //if (id == null)
            //{
            //    return NotFound();
            //}

            //OperationStep operationStep = await _context.OperationStep.Where(w => w.OperationId == id)
            //    .Include(o => o.Operation)
            //    .Include(o => o.Operation.ProductLine)
            //    .Include(o => o.Operation.ProductType)
            //    .FirstOrDefaultAsync();
            //if (operationStep == null)
            //{
            //    return NotFound();
            //}


            //SettingModel model = new()
            //{
            //    OperationStep = operationStep,
            //};

            //return View(model);
        }

        // GET: OperationSteps/Create
        public IActionResult Create()
        {
            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "OperationSteps",
                        Action = "Index",
                        Title = "Back",
                        Icon = "icon-reply",
                        Visible = true
                    },
                    new ToolBarActionLink
                    {
                        //Controller = "OperationSteps",
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

            OperationStepModel model = new() { ToolBar = tool };
            return View(model);
        }

        // POST: OperationSteps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OperationStepId,OperationId,ProductSizeId,Name,DrawingPosition,Description,Sequence,StandardHour,Diameter")] OperationStep operationStep)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operationStep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(operationStep);
        }

        // GET: OperationSteps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OperationStepModel model = await GetDetailAsync(id);

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "OperationSteps",
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



            //if (id == null)
            //{
            //    return NotFound();
            //}

            //OperationStep operationStep = await _context.OperationStep.Where(w=>w.OperationId == id)
            //    .Include(o=>o.Operation)
            //    .Include(o=>o.Operation.ProductLine)
            //    .Include(o=>o.Operation.ProductType)
            //    .FirstOrDefaultAsync();
            //if (operationStep == null)
            //{
            //    return NotFound();
            //}

            //List<Operation> operations = await _context.Operation.Where(w=> operationStep.Operation.ProductLineId == w.ProductLineId && operationStep.Operation.ProductTypeId == w.ProductTypeId).ToListAsync();
            //List<ProductSize> sizes = await _context.ProductSize.Where(w=>w.Size != null).ToListAsync();

            //SettingModel model = new()
            //{
            //    OperationStep = operationStep,
            //    Operations = operations,
            //    ProductSizes = sizes
            //};

            //return View(model);
        }

        // POST: OperationSteps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OperationStepId,OperationId,ProductSizeId,Name,DrawingPosition,Description,Sequence,StandardHour,Diameter")] OperationStep operationStep)
        {
            if (id != operationStep.OperationStepId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    operationStep.OperationStepId = id; //NAVIC!!!!!!!!
                    _context.Update(operationStep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationStepExists(operationStep.OperationStepId))
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
            return View(operationStep);
        }

        

        // GET: OperationSteps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OperationStepModel model = await GetDetailAsync(id);

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "OperationSteps",
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

        // POST: OperationSteps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var operationStep = await _context.OperationStep.FindAsync(id);
            if (operationStep != null)
            {
                //_context.OperationStep.Remove(operationStep);
                operationStep.IsDeleted = true;
                _context.OperationStep.Update(operationStep);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperationStepExists(int id)
        {
            return _context.OperationStep.Any(e => e.OperationStepId == id);
        }
    }
}
