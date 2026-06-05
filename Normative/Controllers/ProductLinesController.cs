using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Normative.Data;
using Normative.Models.Screen;
using Normative.Models.Table;
using X.PagedList.Extensions;

namespace Normative.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductLinesController : Controller
    {
        private readonly NormativeContext _context;

        public ProductLinesController(NormativeContext context)
        {
            _context = context;
        }

        // GET: ProductLines
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
                        Controller = "ProductLines",
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


            ProductLineModel model = new()
            {
                ProductLines = _context.ProductLine.Where(w => w.IsDeleted == false).ToPagedList(),
                ToolBar = tool
            };

            return View(model);
        }

        private async Task<ProductLineModel> GetDetailAsync(int? id)
        {
            var productLine = await _context.ProductLine.FindAsync(id);
            ProductLineModel model = new()
            {
                ProductLine = productLine
            };
            return model;
        }

        // GET: ProductLines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductLineModel model = await GetDetailAsync(id);

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "ProductLines",
                        Action = "Index",
                        Title = "Back",
                        Icon = "icon-reply",
                        Visible = true
                    },
                    new ToolBarActionLink
                    {
                        Controller = "ProductLines",
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

        // GET: ProductLines/Create
        public IActionResult Create()
        {
            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "ProductLines",
                        Action = "Index",
                        Title = "Back",
                        Icon = "icon-reply",
                        Visible = true
                    },
                    new ToolBarActionLink
                    {
                        //Controller = "ProductLine",
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

            ProductLineModel model = new() { ToolBar = tool };
            return View(model);
        }

        // POST: ProductLines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductLineId,Name")] ProductLine productLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productLine);
        }

        // GET: ProductLines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductLineModel model = await GetDetailAsync(id);

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "ProductLines",
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

        // POST: ProductLines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductLineId,Name")] ProductLine productLine)
        {
            if (id != productLine.ProductLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductLineExists(productLine.ProductLineId))
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
            return View(productLine);
        }

        // GET: ProductLines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductLineModel model = await GetDetailAsync(id);

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "ProductLines",
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

        //POST: ProductLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var productLine = await _context.ProductLine.FindAsync(id);
            if (productLine != null)
            {
                //_context.ProductLine.Remove(productLine);
                productLine.IsDeleted = true;  
                _context.ProductLine.Update(productLine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductLineExists(int id)
        {
            return _context.ProductLine.Any(e => e.ProductLineId == id);
        }
    }
}
