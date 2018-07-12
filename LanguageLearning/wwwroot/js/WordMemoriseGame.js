//Variables for scoreboard
var scoreOfCorrect = 0;
var scoreOfWrong = 0;

//Variables for timer
var tens = 0;
var seconds = 0;
var minutes = 0;
var interval;

//Variables for game mode selection
var languageSelection;
var languageFrom;
var languageTo;

checkGameStatus();

$('#gameResetButton').click(function () { 
    //Reset score and progress
    if (confirm("Resetting game, are you sure?")) { // Pop up box for confirmation
        scoreOfCorrect = 0;
        scoreOfWrong = 0;
        stopTimer();
        resetTimer();        
        resetGame();
        checkGameStatus();   
    }      
});

$('.languageButton').click(function () {
    //Initial language selection
    var language = $(this).html();
    var $currentButton = $(this);

    languageSelection = language;

    resetGame();   
    gameTypeSelection(language);
    disableButton('noJQuery', $currentButton);

    $('#startButton').hide();
});

$(".translateTypeButton").click(function () {    
    //Which translation mode selection
    var translateMode = $(this).html();
    var $currentButton = $(this);
    
    $(".translateTypeButton").each(function () {
        if (!$(this).hasClass("hover")) {
            $(this).prop("disabled", false);
            $(this).toggleClass('noJQuery hover');
        }
    });   

    if (translateMode.startsWith('English')) {
        languageTo = languageSelection;
        languageFrom = "English";        
    } else if (translateMode.startsWith(languageSelection)) {
        languageFrom = languageSelection;
        languageTo = "English";        
    }
           
    disableButton('noJQuery', $currentButton);
    $('#startButton').html("Start game");
    $('#startButton').show();  
});

$('#startButton').click(function () {   
    var $currentButton = $(this);
    disableButton('noJQuery', $currentButton);
    startGame();
    checkGameStatus();
});

$('.answerButton').click(function () {
    //Response to any of the answer buttons clicked
    var wordToAnswer = $('#randomWordText').html();        
    var answerValue = $(this).html();
    var answer = answerValue;
    var selectedButton = $(this);

    //Checks if word starts with 'To' and trims the word
    if (answerValue.startsWith("To ")) {
        answerValue = answerValue.substr(3);
    }   
    
    $.get("/Japanese/MemoriseGame?handler=CheckAnswer", {Question: wordToAnswer, Answer: answerValue}).done(function (result) { //When passing data the names have to be the same in C# method               
        var isCorrectObject = JSON.parse(JSON.stringify(result));
               
        if (isCorrectObject.answerCheck) {
            $('#isAnswerCorrect').html("Yes"); 
            answerButtonColorToggle('Correct', selectedButton);  

            calculateScore(true);
            recordTimeStamp(wordToAnswer, answer);
            //alert(answerValue);
            checkGameModeForNextWord();                       
            checkGameStatus();
        }
        else {
            $('#isAnswerCorrect').html("No");
            answerButtonColorToggle('Incorrect', selectedButton);           
            disableButton('incorrect', selectedButton);

            calculateScore(false);
            checkGameStatus();
        }
    });            
});


function resetGame() {      
    //Reset button functionality for other language buttons
    $(".languageButton").each(function () {
        if (!$(this).hasClass("hover")) {
            $(this).prop("disabled", false);
            $(this).toggleClass('noJQuery hover');
        }
    });

    //Reset functionality for translation type buttons
    $(".translateTypeButton").each(function () {
        if (!$(this).hasClass("hover")) {
            $(this).prop("disabled", false);
            $(this).toggleClass('noJQuery hover');
        }
    });   

    //Reset functionality for start button
    if (!$('#startButton').hasClass("hover")) {
        $('#startButton').prop("disabled", false);
        $('#startButton').toggleClass('noJQuery hover');
    }

    $('#startButton').html('Start game');
    $('#startButton').hide();

    $(".languageOption").hide();   

    $('#game').hide();
    $('#gameType').show();    
}

function startGame() {
    $('#startButton').html("Loading..");
    $('#gameTypeDisplay').html(languageFrom + " to " + languageTo); 
    checkGameModeForNextWord();
    checkGameStatus();
}

function gameTypeSelection(language) {   
    $(".languageOption #toEnglish").html(language + " to English");
    $(".languageOption #toJapanese").html("English to " + language);
    $(".languageOption").show();    
}

function disableButton(classToToggle, currentButton) {
    //Disabling button properties
    $(currentButton).prop("disabled", true);

    //Toggling button css properties
    if (classToToggle === 'noJQuery') {
        $(currentButton).toggleClass(classToToggle + ' hover');
    } else if (classToToggle === 'incorrect' || classToToggle === 'correct') {       
        $(currentButton).css('cursor', 'default');               
    }
   
}

function answerButtonColorToggle(answerState, selectedButton) {
    if (answerState === 'Incorrect') {
        if ($(selectedButton).hasClass('neutral')) {
            $(selectedButton).toggleClass('neutral incorrect');
        }
    } else if (answerState === 'Correct') {
        if ($(selectedButton).hasClass('neutral')) {
            $(selectedButton).toggleClass('neutral correct');
        }
    }  
}

function checkGameModeForNextWord() { //Gets next set of words based off game mode  
    if (languageFrom === "English") {
        //Translating from english
        switch (languageTo) {
            case "Japanese":
                alert('Feature currently unavailable');
                resetGame();
                break;
            case "Korean":
                alert('Feature currently unavailable');
                resetGame();
                break;
        }
    } else {
        //Translating to English
        switch (languageFrom) {
            case "Japanese":
                getNextWordJapaneseToEnglish();
                break;
            case "Korean":
                alert('Feature currently unavailable');
                resetGame();
                break;
        }
    }       
}

function checkGameModeForAnswer() { //Checks answer based off game mode
    if (languageFrom === "English") {
        //Translating from english
        switch (languageTo) {
            case "Japanese":
                alert('Feature currently unavailable');
                resetGame();
                break;
            case "Korean":
                alert('Feature currently unavailable');
                resetGame();
                break;
        }
    } else {
        //Translating to English
        switch (languageFrom) {
            case "Japanese":
                checkAnswerJapaneseToEnglish();
                break;
            case "Korean":
                alert('Feature currently unavailable');
                resetGame();
                break;
        }
    }       
}

function getNextWordJapaneseToEnglish() {
    $.get("/Japanese/MemoriseGame?handler=JapanWord", function (words) {
        var randomisedIndex = getRandomIndex();
        selectedWordObject = JSON.parse(JSON.stringify(words)); //Converts JSON into JavaScript object        

        $('#randomWordText').html(selectedWordObject[randomisedIndex].name); //Must use .html to display the object        
        $('#randomWordText').show();

        $('.answerButton').each(function (index) {
            if (selectedWordObject[index].partsOfSpeech === "Verb") {
                $(this).html("To " + selectedWordObject[index].definition);
            } else {
                $(this).html(selectedWordObject[index].definition);
            }
        });
        refreshGameDisplay();
    });
}

function getNextWordEnglishToJapanese() {

}

function getNextWordKoreanToEnglish() {

}

function getNextWordEnglishToKorean() {

}

function checkAnswerJapaneseToEnglish() {
    
}

function checkAnswerEnglishToJapanese() {

}

function checkAnswerKoreanToEnglish() {

}

function checkAnswerEnglishToKorean() {

}

function refreshGameDisplay() {
    //Ensures that the game state does not suddenly change
    resetTimer();
    setTimer();

    $(".answerButton").each(function () {
        var $currentButton = $(this);

        if ($($currentButton).hasClass("incorrect")) {
            $($currentButton).prop("disabled", false);
            $($currentButton).toggleClass('incorrect neutral');
        } else if ($($currentButton).hasClass("correct")) {
            $($currentButton).prop("disabled", false);
            setTimeout(function () {
                var current = $currentButton;
                $(current).toggleClass('correct neutral');                
            }, 900);                    
        }
    });

    $('#gameType').hide();
    $('#game').show();
}

function getRandomIndex() {
    return Math.trunc(Math.random() * 4);
}

function checkGameStatus() {         
    $('#answerScore').html('Correct Answers: ' + scoreOfCorrect + ' | Wrong Answers: ' + scoreOfWrong);

    $('#timerTens').html('00');
    $('#timerSeconds').html('00');
    $('#timerMinutes').html('00');
}

function calculateScore(isCorrectAnswer) {
    if (isCorrectAnswer) {        
        scoreOfCorrect += 1;
    }
    else {
        scoreOfWrong += 1;
    }  
}

function setTimer() {    
    clearInterval(interval);
    interval = setInterval(startTimer, 10);
}

function startTimer() {
    tens++;

    if (tens < 9) {
        $('#timerTens').html("0" + tens); 
    }

    if (tens > 9) {
        $('#timerTens').html(tens);
    }

    if (tens > 99) {       
        seconds++;
        $('#timerSeconds').html("0" + seconds);  
        tens = 0;
        $('#timerTens').html("0" + 0);
    }    

    if (seconds > 9) {
        $('#timerSeconds').html(seconds); 
    }

    if (seconds > 60) {
        minutes++;
        $('#timerMinutes').html("0" + minutes);
        seconds = 0;
        $('#timerSeconds').html("0" + 0);
    }
}

function stopTimer() {
    clearInterval(interval);
}

function resetTimer() {
    clearInterval(interval);
    tens = 0;
    seconds = 0;
    minutes = 0;
   
    $('#timerTens').html('00');
    $('#timerSeconds').html('00');
    $('#timerMinutes').html('00');
}

function recordTimeStamp(question, answer) {      
    var timeStamp;

    if (minutes === 0) {
        timeStamp = seconds + '.' + tens + ' seconds';
    }
    else {
        timeStamp = minutes + ' minutes| ' + seconds + '.' + tens  + ' seconds';
    }
    
    $('#timerHistoryField').append('<p>' + question + ' => ' + answer + " || " + timeStamp + '</p>');
}