function previewImage(input) {
    var file = input.files[0];
    var imagePreview = document.getElementById('image-preview');

    if (file) {
        var reader = new FileReader();
        reader.onload = function (e) {
            imagePreview.src = e.target.result;
            imagePreview.style.display = 'block'; // Show the image preview
        };
        reader.readAsDataURL(file);
    } else {
        imagePreview.src = '';
        imagePreview.style.display = 'none'; // Hide the image preview
    }
}