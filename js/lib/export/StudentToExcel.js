
function exportToExcel() {
    var tab_text = "<table border='2px'>";
    var j = 0;
    tab = document.getElementById("myTable"); 

    for (j = 0; j < tab.rows.length; j++) {

        tab_text = tab_text + "<tr>"+tab.rows[j].innerHTML.toUpperCase() + "</tr>";
        
    }

    tab_text = tab_text + "</table>";

    
    sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
    

    return (sa);
}