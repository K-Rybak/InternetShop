$(document).ready(function () {
    $('.summernote').summernote({
        height: 250
    });
});
function validateInput() {
    if (document.getElementById("uploadBox").value == "") {
        swal({
            title: "Ошибка",
            text: "Пожалуста загрузите изображение",
            icon: "error",
        });
        return false;
    }
    return true;
}