

function validate() {
    var formAdd = document.getElementById("formAddMessage");
    var res = true;

    if (formAdd.content.value.trim().length == 0) {
        res = false;
        document.getElementById("contentWarning").innerHTML = "không được để trống";
    }
    else
        document.getElementById("contentWarning").innerHTML = "";
    
   
    return res;
}

