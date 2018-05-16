

function validate() {
    var formAdd = document.getElementById("formSubmitHomework");
    var deadLine = new Date(formAdd.deadLine.value);
    var res = true;
    
    if (new Date() > deadLine) {
        res = false;
        document.getElementById("deadLineWarning").innerHTML = "đã quá hạn";
    } else {
        document.getElementById("deadLineWarning").innerHTML = "";
    }

    if (formAdd.homework.value.trim().length == 0) {
        res = false;
        document.getElementById("homeworkWarning").innerHTML = "phải có file kèm";
    } else {
        document.getElementById("homeworkWarning").innerHTML = "";
    }

    return res;
}

function isInt(value) {
    var er = /^[0-9]+$/;
    return er.test(value);
}