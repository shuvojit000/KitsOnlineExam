
$(document).ready(function () {
    
    $('body').belowfold();

    $('#toggleTime').click(function () {
        $("#showTimePanel").slideToggle("fast");
        if ($.cookie('timecollapsed') === 'on') {
            $.cookie('timecollapsed', 'off');
        } else {
            $.cookie('timecollapsed', 'on');
        }
        checkTimePnlCookie();
    });
    
    $('#toggleAnswered').click(function () {
        $("#answeredPanel").slideToggle("fast");
        if ($.cookie('answeredcollapsed') === 'on') {
            $.cookie('answeredcollapsed', 'off');
        } else {
            $.cookie('answeredcollapsed', 'on');
        }
        checkAnsweredPnlCookie();
    });

    $('#lblUnansweredValue').click(function () {
        var pnlIsVisible = $('#ShowUnAnsweredPanel').is(":visible");
        var answeredCount = $('#lblUnansweredValue').text();

        if (unansweredCount === "0") {
            return false;
        }

        if (!$("#lblUnansweredValue").is('[disabled=disabled]')) {
            if (answeredCount !== "0") {
                if ($('#ShowAnsweredPanel').is(":visible")) {
                    $('#ShowAnsweredPanel').toggle('slide', 'left', 400, function () {
                        if (!pnlIsVisible) {
                            $('#ShowUnAnsweredPanel').toggle('slide', 'right', 400);
                        } else {
                            $('#ShowUnAnsweredPanel').toggle('slide', 'left', 400);
                        }
                    });
                } else if ($('#ShowFlaggedAnsweredPanel').is(":visible")) {
                    $('#ShowFlaggedAnsweredPanel').toggle('slide', 'left', 400, function () {
                        if (!pnlIsVisible) {
                            $('#ShowUnAnsweredPanel').toggle('slide', 'right', 400);
                        } else {
                            $('#ShowUnAnsweredPanel').toggle('slide', 'left', 400);
                        }
                    });
                } else {
                    if (!pnlIsVisible) {
                        $('#ShowUnAnsweredPanel').toggle('slide', 'right', 400);
                    } else {
                        $('#ShowUnAnsweredPanel').toggle('slide', 'left', 400);
                    }
                }
            }
        }
        
    });

    $('#CloseUnAnsweredPanel').click(function () {
        var pnlIsVisible = $('#ShowUnAnsweredPanel').is(":visible");
        if (!pnlIsVisible) {
            $('#ShowUnAnsweredPanel').toggle('slide', 'right', 400);
        } else {
            $('#ShowUnAnsweredPanel').toggle('slide', 'left', 400);
        }
    });

    $('#lblAnsweredValue').click(function () {
        var pnlIsVisible = $('#ShowAnsweredPanel').is(":visible");
        var answeredCount = $('#lblAnsweredValue').text();

        if (answeredCountValue === "0") {
            return false;
        }

        if (!$("#lblAnsweredValue").is('[disabled=disabled]')) {
            if (answeredCount !== "0") {
                if ($('#ShowUnAnsweredPanel').is(":visible")) {
                    $('#ShowUnAnsweredPanel').toggle('slide', 'left', 400, function () {
                        if (!pnlIsVisible) {
                            $('#ShowAnsweredPanel').toggle('slide', 'right', 400);
                        } else {
                            $('#ShowAnsweredPanel').toggle('slide', 'left', 400);
                        }
                    });
                } else if ($('#ShowFlaggedAnsweredPanel').is(":visible")) {
                    $('#ShowFlaggedAnsweredPanel').toggle('slide', 'left', 400, function () {
                        if (!pnlIsVisible) {
                            $('#ShowAnsweredPanel').toggle('slide', 'right', 400);
                        } else {
                            $('#ShowAnsweredPanel').toggle('slide', 'left', 400);
                        }
                    });
                } else {
                    if (!pnlIsVisible) {
                        $('#ShowAnsweredPanel').toggle('slide', 'right', 400);
                    } else {
                        $('#ShowAnsweredPanel').toggle('slide', 'left', 400);
                    }

                }
            }
        }
        
    });
    
    $('#CloseAnsweredPanel').click(function () {
        var pnlIsVisible = $('#ShowAnsweredPanel').is(":visible");
        if (!pnlIsVisible) {
            $('#ShowAnsweredPanel').toggle('slide', 'right', 400);
        } else {
            $('#ShowAnsweredPanel').toggle('slide', 'left', 400);
        }
    });

    $('#lblFlaggedValue').click(function () {
        var pnlIsVisible = $('#ShowFlaggedAnsweredPanel').is(":visible");
        var answeredCount = $('#lblFlaggedValue').text();

        if (flaggegCountValue === "0") {
            return false;
        }

        if (!$("#lblFlaggedValue").is('[disabled=disabled]')) {
            if (answeredCount !== "0") {
                if ($('#ShowAnsweredPanel').is(":visible")) {
                    $('#ShowAnsweredPanel').toggle('slide', 'left', 400, function () {
                        if (!pnlIsVisible) {
                            $('#ShowFlaggedAnsweredPanel').toggle('slide', 'right', 400);
                        } else {
                            $('#ShowFlaggedAnsweredPanel').toggle('slide', 'left', 400);
                        }
                    });
                } else if ($('#ShowUnAnsweredPanel').is(":visible")) {
                    $('#ShowUnAnsweredPanel').toggle('slide', 'left', 400, function () {
                        if (!pnlIsVisible) {
                            $('#ShowFlaggedAnsweredPanel').toggle('slide', 'right', 400);
                        } else {
                            $('#ShowFlaggedAnsweredPanel').toggle('slide', 'left', 400);
                        }
                    });
                } else {
                    if (!pnlIsVisible) {
                        $('#ShowFlaggedAnsweredPanel').toggle('slide', 'right', 400);
                    } else {
                        $('#ShowFlaggedAnsweredPanel').toggle('slide', 'left', 400);
                    }
                }
            }
        }

    });

    $('#CloseFlaggedAnsweredPanel').click(function () {
        var pnlIsVisible = $('#ShowFlaggedAnsweredPanel').is(":visible");
        if (!pnlIsVisible) {
            $('#ShowFlaggedAnsweredPanel').toggle('slide', 'right', 400);
        } else {
            $('#ShowFlaggedAnsweredPanel').toggle('slide', 'left', 400);
        }

    });

    function checkTimePnlCookie() {
        if ($.cookie('timecollapsed') === 'on') {

            //hide panel and update classes
            $('#showTimePanel').hide();

            $('#toggleTime').removeClass('fa fa-minus-square');
            $('#toggleTime').addClass('fa fa-plus-square');
             
            $('#timePanel').addClass('timePnlHghHide');
        }
        else {

            $('#showTimePanel').show();  

            $('#toggleTime').removeClass('fa fa-plus-square');
            $('#toggleTime').addClass('fa fa-minus-square');

            $('#timePanel').removeClass('timePnlHghHide');

        }
    }

    function checkAnsweredPnlCookie() {
        if ($.cookie('answeredcollapsed') === 'on') {

            //hide panel and update classes
            $('#answeredPanel').hide();

            $('#toggleAnswered').removeClass('fa fa-minus-square');
            $('#toggleAnswered').addClass('fa fa-plus-square');

            $('#totalAnswersPanel').removeClass('totalAnswers');
            $('#lblTotal').removeClass('totalAnswers');
            $('#totalAnswersPanel').addClass('totalAnswersHeight');
            $('#allAnsweredPanel').addClass('adjustAllAnsweredHeight');

        } else {
            $('#answeredPanel').show();

            $('#toggleAnswered').removeClass('fa fa-plus-square');
            $('#toggleAnswered').addClass('fa fa-minus-square');

            $('#totalAnswersPanel').addClass('totalAnswers'); 
            $('#lblTotal').addClass('totalAnswers');
            $('#allAnsweredPanel').removeClass('adjustAllAnsweredHeight');

        }
    }
    
    //check cookie values and update
    checkTimePnlCookie();
    checkAnsweredPnlCookie();
   
    $('#btnLogout').click(function () {
        $('#confirmLogoutPop').popup('show');
        return false;
    });

    $('#cancelSubmitPop').popup({
        onopen: function () {
            //this has to be added here for ie8
            $(this).addClass('elementHover');
            $('#submitButtonPanel').addClass('elementHover');
        },
        onclose: function () {
        }
    });

    $('#confirmLogoutPop').popup({
        onopen: function () {
        },
        onclose: function () {
        }
    });
    
    $('#cancelSubmit').click(function () {
        $('#cancelSubmitPop').popup('hide');
    }); 

    $('#cancelLogout').click(function () {

            $('#confirmLogoutPop').popup('hide');

    }); 

    $('#saveAndLogout').click(function () {
            var redirectValue = angElements.redirectLocation;
            $.cookie('redirectUrl', redirectValue);
    });

    //this handles when the "X" is clicked in the I UNDERSTAND txtbox for IE
    $("#confSubmitInputBox").bind("mouseup", function () {
        var $input = $(this),
        existingValue = $input.val();

        if (existingValue === "") {
            return;
        }

        setTimeout(function () {
            var clearedValue = $input.val();
            if (clearedValue === "") {
                $input.trigger("cleared");
                $('#submitButtonPanel').removeClass('casSubmitButton2');
                $('#submitButtonPanel').addClass('casSubmitButtonDisabled');
                
                angular.element(document.getElementById('mainCtrl')).scope().Iex();
            }
        }, 1);
    });

    var isIpad = navigator.userAgent.match(/iPad/i) !== null;
    if (isIpad) {
        
        $('#castleHeader').css("width", "965px");
        
        $('#txtTimer').css("margin-left", "1px");
        $('#txtTimer').css("width", "100%");

        $('#QuestionContainer').css("width", "790px");
        $('#navContainer').css("margin-right", "78px");
    }
});


