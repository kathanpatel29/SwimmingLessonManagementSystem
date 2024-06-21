window.onload = function() {
  var currentIndex = 0;
  var images = document.querySelectorAll('.carousel-images .gallery-image-container');
  var totalImages = images.length;
  var numVisible = 4; // Number of images visible at once

  function prevSlide() {
    currentIndex = Math.max(currentIndex - numVisible, 0);
    updateCarousel();
  }

  function nextSlide() {
    currentIndex = Math.min(currentIndex + numVisible, totalImages - numVisible);
    updateCarousel();
  }

  function updateCarousel() {
    images.forEach(function(image, index) {
      if (index >= currentIndex && index < currentIndex + numVisible) {
        image.style.display = 'block'; // Show images within the visible range
      } else {
        image.style.display = 'none'; // Hide images outside the visible range
      }
    });
  }

  var nextButton = document.getElementById('next-button');
  nextButton.onclick = nextSlide;

  var preButton = document.getElementById('pre-button');
  preButton.onclick = prevSlide;

  // Initialize carousel
  updateCarousel();
};
