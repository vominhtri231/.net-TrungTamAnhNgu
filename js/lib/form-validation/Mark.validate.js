

function validate() {
    var formAdd = document.getElementById("formMark");
    var res = true;
   

    if (formAdd.score.value.length == 0) {
        res = false;
        document.getElementById("scoreWarning").innerHTML = "không được để trống";
    } else {
        if (!isInt(formAdd.score.value)) {
            res = false;
            document.getElementById("scoreWarning").innerHTML = "không đúng định dạng";
        } else
            document.getElementById("scoreWarning").innerHTML = "";
    } 

    return res;
}

function isInt(value) {
    var er = /^[0-9]+$/;
    return er.test(value);
}