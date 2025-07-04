using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using BlogStore.PresentationLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IToxicityDetectionService _toxicityDetectionService;
        private readonly ITranslationService _translationService;

        public CommentController(ICommentService commentService, UserManager<AppUser> userManager, IToxicityDetectionService toxicityDetectionService, ITranslationService translationService)
        {
            _commentService = commentService;
            _userManager = userManager;
            _toxicityDetectionService = toxicityDetectionService;
            _translationService = translationService;
        }

        public IActionResult CommentList()
        {
            var values = _commentService.TGetAll();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateComment()
        {
            return View();
        } 
        [HttpPost]
        public async Task<IActionResult> CreateComment(Comment comment)
        {
            //comment.CommentDate= DateTime.Now;
            //comment.IsValid = false;
            //_commentService.TInsert(comment);
            //return RedirectToAction("CommentList");

            try
            {
                if (comment == null || string.IsNullOrWhiteSpace(comment.CommentDetail))
                    return Json(new { status = "error", message = "Yorum verisi eksik veya geçersiz" });

                var translatedCommentDetail= await _translationService.TranslateToEnglishAsync(comment.CommentDetail);

                var detectionResult= await _toxicityDetectionService.DetecToxicityAsync(translatedCommentDetail);
                comment.IsToxic = detectionResult.IsToxic;
                comment.ToxicityScore = (float)detectionResult.Score;
                comment.AppUserId = _userManager.GetUserId(User);
                comment.UserNameSurname = _userManager.GetUserName(User);
                comment.CommentDate= DateTime.Now;

                _commentService.TInsert(comment);
                if (detectionResult.IsToxic)
                    return Json(new { status = "toxic", message = "Yorumunuz toksik içerik barındırıyor." });
                return Json(new { status = "success", message = "Yorumunuz Başarıyla eklendi." });
            }
            catch (Exception ex)
            {

                return Json(new { status = "error", message = $"Bir Hata Oluştu: {ex.Message}" });
            }
        }

        public IActionResult DeleteComment(int id)
        {
            _commentService.TDelete(id);
            return RedirectToAction("CommentList");
        }

        [HttpGet]
        public IActionResult UpdateComment(int id)
        {
            var value=_commentService.TGetById(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateComment(Comment comment)
        {
            comment.CommentDate= DateTime.Now;
            comment.IsValid = false;
            _commentService.TUpdate(comment);
            return RedirectToAction("CommentList");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCommentAjax(int articleId, string commentDetail)
        {
            try
            {
                if (string.IsNullOrEmpty(commentDetail) || commentDetail.Length < 3)
                {
                    return Json(new { success = false, message = "Yorum en az 3 karakter olmalıdır." });
                }


                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (user == null)
                {
                    return Json(new { success = false, message = "Kullanıcı bulunamadı." });
                }


                var comment = new Comment
                {
                    ArticleId = articleId,
                    CommentDetail = commentDetail,
                    CommentDate = DateTime.Now,
                    AppUserId = user.Id,
                    UserNameSurname = user.Name + " " + user.Surname,
                    IsValid = true // Login olan kullanıcılar için otomatik onay
                };

                _commentService.TInsert(comment);


                return Json(new
                {
                    success = true,
                    message = "Yorumunuz başarıyla eklendi!",
                    comment = new
                    {
                        userNameSurname = comment.UserNameSurname,
                        commentDetail = comment.CommentDetail,
                        commentDate = comment.CommentDate.ToString("dd-MMM-yyyy"),
                        userImageUrl = user.ImageUrl
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Bir hata oluştu: " + ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetCommentsByArticle(int articleId)
        {
            var comments = _commentService.TGetCommentsByArticle(articleId);
            var commentList = comments.Select(c => new {
                userNameSurname = c.AppUser.Name + " " + c.AppUser.Surname,
                commentDetail = c.CommentDetail,
                commentDate = c.CommentDate.ToString("dd-MMM-yyyy"),
                userImageUrl = c.AppUser.ImageUrl
            });

            return Json(new { success = true, comments = commentList });
        }
    }
}
