document.onload = getMovies()

///////////
// Get Movies
///////////

function getMovies() {

  $.get("https://dwmoviesapi.azurewebsites.net/api/movies", function(data){
    data.map(function(m){
      $("#moviedata").append("<div class=" + "col-md-4" + "><img src=" + m.imageUrl + ">" + "<h4>" + m.title + "</h4>" + "<p>" + m.genre + "</p>" + "</div>");
    })
  });
}