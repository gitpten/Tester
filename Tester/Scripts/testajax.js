

$(function () {
    $("#Next").disabled = true;

    $("input:radio").bind("change click", function () {
        $("#Next").disabled = false;
    });

    $("#formAnswers").submit(function () {
        if ($("#timespent").html() <= 0 || $("#Questionsleft").html() == 1) {
            return true;
        }
        var Answer = new Object();
        Answer.QuestionID = $("#QuestionID").val();
        Answer.NumOptionAnswer = $(":radio[name=NumOptionAnswer]").filter(":checked").val()

        $.ajax({
            type: 'POST',
            url: $(this).attr("action"),
            data: Answer,
            success: function (data) {
                $("#QuestionText").html(data.Text);
                $("#HiQuestionID").html('<input id="QuestionID" name="QuestionID" type="hidden" value="' + data.QuestionID + '" />');

                var code = '';
                $(data.OptionAnswers).each(function (i) {
                    code += '<div id="Answer"><input type="radio" id="NumOptionAnswer" name="NumOptionAnswer" value="' + i + '" />' + $(this).val() + '</div>';
                });
                $("#Answers").html(code);
            }
        });

        $("Questionsleft").html(this--);
        uncheckAllRadio("NumOptionAnswer");
        $("#Next").disabled = true;

        return false;
    });

    function uncheckAllRadio(nameElement) {
        var obj = $('*[name = nameElement]');
        for (i = 0; i < obj.length; i++)
            obj[i].checked = false;
    };
});



