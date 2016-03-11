
$(document).ready(function() {
  $(".btn-message").click(function(event) {
    $(".message-modal").fadeToggle();
    $(".modal-transparency").fadeToggle();
  });
  $(".modal-close").click(function(event) {
    $(".message-modal").fadeToggle();
    $(".modal-transparency").fadeToggle();
  });
  
  $('.menu-toggle').click(function(event) {
    $(this).siblings().slideToggle();
  });

  $(window).resize(function() {
    var windowSize = $(window).width();
    if (windowSize > 970) {
      $(".menu-toggle").siblings().show();
    }
    else {
        $(".menu-toggle").siblings().hide();
      }
  });
});


// $(window).resize(function() {
//
// })
