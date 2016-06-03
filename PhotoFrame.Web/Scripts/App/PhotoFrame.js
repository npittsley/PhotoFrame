
//Hides/Shows Gallery vs Grid View of Photos
var togglePhotoView = function () {
    $(".photoGallery").toggle("slow", function () {
        // Animation complete.
    });
    $(".photoGrid").toggle("slow", function () {
        // Animation complete.
    });
};