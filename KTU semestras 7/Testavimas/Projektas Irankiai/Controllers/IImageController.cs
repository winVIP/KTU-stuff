using Projektas_Irankiai.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Projektas_Irankiai.Controllers
{
    public interface IImageController
    {
        ActionResult GeneratedImagesView();
        List<int> GetAllID();
        List<TradeOffer> GetAllTradeOffers();
        List<Image> GetImages();
        List<TradableImage> GetInvImages();
        int GetMaxID();
        List<int> GetOffersIDs();
        List<Image> GetRecommendedImages();
        List<Image> GetTemplates();
        List<TradeOffer> GetTradeOffers();
        List<User> GetUsers();
        ActionResult ImageCreationView();
        ActionResult ImageCreationView(selectImageAndText selectImageAndText);
        ActionResult ImageDeletion(Image image);
        ActionResult ImageDeletion(int id);
        ActionResult ImageEdit(Image image);
        ActionResult ImageEdit(int id);
        ActionResult ImageUpload();
        ActionResult ImageUpload(Image imageModel);
        ActionResult Index();
        ActionResult Inventory();
        ActionResult rateImage(int id);
        ActionResult RecommendedList();
        ActionResult UploadedImagesView();
    }
}