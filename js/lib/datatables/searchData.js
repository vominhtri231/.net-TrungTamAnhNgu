function search(){

    var searchValue = document.getElementById("searchInput").value;
    if (searchValue == "") return;

    var table = document.getElementById("myTable");
    var rows = table.getElementsByTagName("tr");
    for (var i = 1; i < rows.length; i++) {
        var tds = rows[i].getElementsByTagName("td");

        var j;
        for (j = 0; j < tds.length; j++) {
            if (tds[j].innerHTML.indexOf(searchValue) > -1) break;
        }

        if (j == tds.length)
            rows[i].style.display = "none";
        else
            rows[i].style.display = "";
    }
}