using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Lekkerbek12Gip.Controllers
{

    [Authorize(Roles = "Admin,Kassamedewerker,Klant")]
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IKlantsService _klantsService;

       
        public ReviewsController(LekkerbekContext context, IReviewService reviewService, IKlantsService klantsService)
        {
            _klantsService = klantsService;
            _reviewService = reviewService;
        }
        [AllowAnonymous]
        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            return View(await _reviewService.GetAllReviews());
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            var klant = _klantsService.Get(x => x.emailadres == User.Identity.Name);
            if (klant != null)
            {
                ViewData["Klant"] = new SelectList(_klantsService.GetKlantenIE(), "KlantId", "Name");
                if (User.IsInRole("Klant"))
                {
                    ViewData["KlantId"] = klant.Result.KlantId;
                }
            }
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,KlantId,ReviewMessage,Score,TimeOfReview")] Review review, string message)
        {
            if (ModelState.IsValid)
            {
                review.ReviewMessage = message;
                await _reviewService.AddReview(review);
                return RedirectToAction(nameof(Index));
            }
            if (User.IsInRole("Klant"))
            {
                var klant = _klantsService.Get(x => x.emailadres == User.Identity.Name);
                ViewData["KlantId"] = klant.Result.KlantId;
            }
            else
            {
                ViewData["KlantId"] = new SelectList(_klantsService.GetKlantenIE(), "KlantId", "Name", review.KlantId);
            }
            
            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _reviewService.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }
            // Check if user is allowed to edit this review
            if (review.Klant.emailadres != User.Identity.Name && User.IsInRole("Klant"))
            {
                return NotFound();
            }
            ViewData["KlantId"] = new SelectList(_klantsService.GetKlantenIE(), "KlantId", "Name", review.KlantId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewId,KlantId,ReviewMessage,Score,TimeOfReview")] Review review, string message)
        {
            if (id != review.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    review.ReviewMessage = message;
                    await _reviewService.UpdateReview(review);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewId))
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
            ViewData["KlantId"] = new SelectList(_klantsService.GetKlantenIE(), "KlantId", "Name", review.KlantId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _reviewService.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }
            // Check if user is allowed to delete this review
            if (review.Klant.emailadres != User.Identity.Name && User.IsInRole("Klant"))
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reviewService.DeleteReview(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _reviewService.ReviewExists(id);
        }
    }
}
