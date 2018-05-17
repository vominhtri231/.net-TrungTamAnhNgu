function sort(event) {

    var element = event.target;
    var className = element.className;
    var field = element.getAttribute('index');

    var table = document.getElementById("myTable");
    var rows = table.getElementsByTagName("tr");
    var headers = rows[0].getElementsByTagName("th");
    for (var i = 0; i < headers.length; i++) {
        headers[i].className = "sorting";

    }

    switch (className) {
        case "sorting":
            element.className = "sorting_asc";
            sorting_asc(rows, field);
            break;
        case "sorting_desc":
            element.className = "sorting_asc";
            sorting_asc(rows, field);
            break;
        case "sorting_asc":
            element.className = "sorting_desc";
            sorting_desc(rows, field);
            break;
    }

}

function sorting_asc(rows, field) {

    for (var i = 1; i < rows.length - 1; i++) {
        for (var j = rows.length - 1; j > i; j--) {
            if (rows[j].getElementsByTagName("td")[field].innerHTML < rows[j - 1].getElementsByTagName("td")[field].innerHTML) {
                rows[j - 1].parentNode.insertBefore(rows[j], rows[j - 1]);
            }
        }
    }
}

function sorting_desc(rows, field) {
    for (var i = 1; i < rows.length - 1; i++) {
        for (var j = rows.length - 1; j > i; j--) {
            if (rows[j].getElementsByTagName("td")[field].innerHTML > rows[j - 1].getElementsByTagName("td")[field].innerHTML) {
                rows[j - 1].parentNode.insertBefore(rows[j], rows[j - 1]);
            }
        }
    }
}