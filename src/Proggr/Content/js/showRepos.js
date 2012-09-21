///
/// displays repositories for a user after they have been 
/// retrieved from the github api. This method also checks to 
/// see if proggr already has these projects in the db.
function showRepos( jsonp ) {
    console.log(jsonp);
    var repositories = jsonp.data;
    var source = $("#repository-template").html();
    var template = Handlebars.compile(source);

    for (var i = -1, l = repositories.length; ++i < l;) {
        var repo = repositories[i];
        var html = template(repo);

        $('#repository-list[rel="showRepos"]').append(html);
    }
}