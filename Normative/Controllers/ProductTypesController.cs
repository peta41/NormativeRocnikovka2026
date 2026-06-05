using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Normative.Data;
using Normative.Models.Screen;
using Normative.Models.Table;
using X.PagedList.Extensions;

namespace Normative.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductTypesController : Controller
    {
        private readonly NormativeContext _context;

        public ProductTypesController(NormativeContext context)
        {
            _context = context;
        }

        // GET: ProductTypes
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
                        Controller = "ProductTypes",
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

            ProductTypeModel model = new()
            {
                ProductTypes = _context.ProductType.Where(w => w.IsDeleted == false).ToPagedList(),
                ToolBar = tool
            };

            return View(model);
        }


        //funkce na ziskani details nasledne pouzivano pro: details, edit, delete.
        private async Task<ProductTypeModel> GetDetailAsync(int? id)
        {
            var productType = await _context.ProductType.FindAsync(id);
            ProductTypeModel model = new()
            {
                ProductType = productType
            };
            return model;
        }

        // GET: ProductTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductTypeModel model = await GetDetailAsync(id);

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "ProductTypes",
                        Action = "Index",
                        Title = "Back",
                        Icon = "icon-reply",
                        Visible = true
                    },
                    new ToolBarActionLink
                    {
                        Controller = "ProductTypes",
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

            //return View(model);
        }

        // GET: ProductTypes/Create
        public IActionResult Create()
        {
            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "ProductTypes",
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

            ProductTypeModel model = new() { ToolBar = tool };
            return View(model);
        }

        // POST: ProductTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductTypeId,Name")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductTypeModel model = await GetDetailAsync(id);

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "ProductTypes",
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
            //return View(productType);
        }

        // POST: ProductTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductTypeId,Name")] ProductType productType)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    productType.ProductTypeId = id;                 
                    _context.Update(productType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(productType.ProductTypeId))
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
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductTypeModel model = await GetDetailAsync(id);

            // Create action link for page
            List<ToolBarActionLink> ListAction = new()
                {
                    new ToolBarActionLink
                    {
                        Controller = "ProductTypes",
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

        //POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var productType = await _context.ProductType.FindAsync(id);
            if (productType != null)
            {
                //_context.ProductType.Remove(productType);
                productType.IsDeleted = true;
                _context.ProductType.Update(productType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTypeExists(int id)
        {
            return _context.ProductType.Any(e => e.ProductTypeId == id);
        }
    }
}
