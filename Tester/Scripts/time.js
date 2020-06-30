$(function () {
    setInterval(getSecs($("#timespent").html()), 1000)
});

startday = new Date();
clockStart = startday.getTime();

function initStopwatch() {
    var myTime = new Date();
    var timeNow = myTime.getTime();
    var timeDiff = timeNow - clockStart;
    this.diffSecs = timeDiff / 1000;
    return (this.diffSecs);
}

function getSecs(testTime) {
    var mySecs = testTime - initStopwatch();

    if (mySecs <= 0) {
        $("#formAnswers").submit();
    }

    var mySecs1 = "" + mySecs;
    mySecs1 = mySecs1.substring(0, mySecs1.indexOf("."));
    $("#timespent").html(mySecs1);
    $(":radio[name=NumOptionAnswer]").filter(":checked").checked = false;
}

