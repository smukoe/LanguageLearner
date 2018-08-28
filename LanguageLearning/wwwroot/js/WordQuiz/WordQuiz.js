//Variables for scoreboard
var scoreOfCorrect = 0;
var scoreOfWrong = 0;

var totalWordCount;
var isRestart = false;
var timerDelay = 500;

//Variables for timer
var tens = 0;
var seconds = 0;
var minutes = 0;
var interval;

//Variables for game mode selection
var languageSelection;
var kanaSelection;
var isKana;
var languageFrom;
var languageTo;
var isPractice = false;

checkGameStatus();

$('#gameResetButton').click(function () { 
    //Reset score and progress
    isRestart = false;
    if (confirm("Resetting game, are you sure?")) { // Pop up box for confirmation
        scoreOfCorrect = 0;
        scoreOfWrong = 0;
        stopTimer();
        resetTimer();        
        resetGame();
        checkGameStatus();   
    }      
});

$('#replayButton').click(function () {
    isRestart = true;
    if (confirm("Starting again, are you sure?")) { // Pop up box for confirmation
        scoreOfCorrect = 0;
        scoreOfWrong = 0;
        stopTimer();
        resetTimer();
        resetGame();
        checkGameStatus();
    }    
});

$('#practice button').click(function () {   
    var $selectedButton = $(this);

    if ($selectedButton.hasClass('off')) {
        isPractice = true;
        $selectedButton.toggleClass('off on');
    } else if ($selectedButton.hasClass('on')) {
        isPractice = false;
        $selectedButton.toggleClass('off on');
    }

    if (isPractice) {
        $('#practiceStatus').html("On");
    } else {
        $('#practiceStatus').html("Off");
    }    
});

$('.languageButton').click(function () {
    //Initial language selection
    var language = $(this).html();
    var $selectedButton = $(this);

    languageSelection = language;

    resetGame();   
    gameTypeSelection(language);
    disableButton('noJQuery', $selectedButton);

    $('#startButton').hide();
});

$(".translateTypeButton").click(function () {    
    //Which translation mode selection
    var translateMode = $(this).html();
    var $selectedButton = $(this);
    
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
           
    disableButton('noJQuery', $selectedButton);
    $('#startButton').html("Start game");

    if (translateMode === "Kana") {
        isKana = true;
        $('#startButton').hide();
        $('.kanaOption').show();       
    } else {       
        isKana = false;
        $(".kanaOption").hide();
        $(".kanaTranslateOption").hide();

        $(".kanaType").each(function () {
            if (!$(this).hasClass("hover")) {
                $(this).prop("disabled", false);
                $(this).toggleClass('noJQuery hover');
            }
        });

        $(".kanaTranslateType").each(function () {
            if (!$(this).hasClass("hover")) {
                $(this).prop("disabled", false);
                $(this).toggleClass('noJQuery hover');
            }
        });   

        $('#startButton').show();  
    }   
});

$('.kanaType').click(function () {
    var kanaChosen = $(this).html();   
    var $selectedButton = $(this);

    kanaSelection = kanaChosen;   

    $(".kanaType").each(function () {
        if (!$(this).hasClass("hover")) {
            $(this).prop("disabled", false);
            $(this).toggleClass('noJQuery hover');
        }
    });   

    $(".kanaTranslateType").each(function () {
        if (!$(this).hasClass("hover")) {
            $(this).prop("disabled", false);
            $(this).toggleClass('noJQuery hover');
        }
    });   

    kanaTypeSelection(kanaChosen);
    disableButton('noJQuery', $selectedButton);    
});

$('.kanaTranslateType').click(function () {
    var translateMode = $(this).html();
    var $selectedButton = $(this);
    
    $(".kanaTranslateType").each(function () {
        if (!$(this).hasClass("hover")) {
            $(this).prop("disabled", false);
            $(this).toggleClass('noJQuery hover');
        }
    });   
    
    if (translateMode.startsWith('English')) {
        languageTo = kanaSelection;
        languageFrom = "English";
    } else if (translateMode.startsWith(kanaSelection)) {
        languageFrom = kanaSelection;
        languageTo = "English";
    }
    
    disableButton('noJQuery', $selectedButton);   
    
    $('#startButton').html("Start game");   
    $('#startButton').show();
});

$('#startButton').click(function () {   
    var $selectedButton = $(this);
    disableButton('noJQuery', $selectedButton);   
    startGame();   
});

$('.answerButton').click(function () {
    //Response to any of the answer buttons clicked
    var wordToAnswer = $('#randomWordText').html();        
    var answerValue = $(this).html();    
    var $selectedButton = $(this);

    disableOrEnableAnswerButtons('Disable');
    checkGameModeForAnswer(wordToAnswer, answerValue, $selectedButton);    
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

    //Reset functionality for kana selection
    $(".kanaType").each(function () {
        if (!$(this).hasClass("hover")) {
            $(this).prop("disabled", false);
            $(this).toggleClass('noJQuery hover');
        }
    });   

    //Reset functionality for kana type buttons
    $(".kanaTranslateType").each(function () {
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
    $('#timerHistoryField .content').empty();

    $('#startButton').html('Start game');
    $('#startButton').hide();

    $(".languageOption").hide(); 
    $(".kanaOption").hide();
    $(".kanaTranslateOption").hide();

    if (isRestart) {
        startGame();
    } else {
        $('#game').hide();
        $('#gameType').show(); 
    } 
}

function startGame() {
    let canStart;
    $('#startButton').html("Loading..");
    $('#gameTypeDisplay').html(languageFrom + " to " + languageTo); 
    setGameMode();    
}

function gameTypeSelection(language) {   
    if (language === "Japanese") {
        $('#kanaButton').show();
    } else {
        $('#kanaButton').hide();
    }

    $(".languageOption #toEnglish").html(language + " to English");
    $(".languageOption #fromEnglish").html("English to " + language);
    $(".languageOption").show();    
}

function kanaTypeSelection(kana) {
    $(".kanaTranslateOption #fromKana").html(kana + " to English");
    $(".kanaTranslateOption #toKana").html("English to " + kana);
    $(".kanaTranslateOption").show();    
}

function disableButton(classToToggle, $selectedButton) {
    //Disabling button properties
    $selectedButton.prop("disabled", true);

    //Toggling button css properties
    if (classToToggle === 'noJQuery') {
        $selectedButton.toggleClass(classToToggle + ' hover');
    } else if (classToToggle === 'incorrect' || classToToggle === 'correct') {       
        $selectedButton.css('cursor', 'default');               
    }   
}

function answerButtonColorToggle(answerState, $selectedButton) {
    if (answerState === 'Incorrect') {
        if ($selectedButton.hasClass('neutral')) {
            $selectedButton.toggleClass('neutral incorrect');
        }
    } else if (answerState === 'PracticeIncorrect') {
        if ($selectedButton.hasClass('neutral')) {
            $selectedButton.toggleClass('neutral practiceIncorrect');
        }
    } else if (answerState === 'Correct') {
        if ($selectedButton.hasClass('neutral')) {
            $selectedButton.toggleClass('neutral correct');
        }
    }      
}

function setGameMode() {
    var data;    

    if (languageFrom !== 'English') {
        switch (languageFrom) {
            case "Japanese":
                data = "Japanese";
                break;
            case "Hiragana":
                data = "Hiragana";
                break;
            case "Katakana":
                data = "Katakana";
                break;
            case "Korean":
                data = "Korean";
                break;
        }
    } else {
        switch (languageTo) {
            case "Japanese":
                data = "Japanese";
                break;
            case "Hiragana":
                data = "Hiragana";
                break;
            case "Katakana":
                data = "Katakana";
                break;
            case "Korean":
                data = "Korean";
                break;
        }
    }

    $.get("/Self_Study/WordQuiz?handler=SetGameType", { mainLanguage: data, isPractice: isPractice }).done(function (result) {
        wordCount = JSON.parse(JSON.stringify(result));
        
        totalWordCount = wordCount.allWordsCount;               
        checkGameModeForNextWord();
        checkGameStatus();         
    });
}

function checkGameModeForNextWord() { //Gets next set of words based off game mode  
    var urlHandler;
    
    if (languageFrom === "English") {
        //Translating from english
        switch (languageTo) {
            case "Japanese":               
                urlHandler = 'JapanWord';
                getNextWord(urlHandler);
                break;
            case "Hiragana":
                urlHandler = 'Hiragana';
                getNextWord(urlHandler);
                break;
            case "Katakana":
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
                urlHandler = 'JapanWord';
                getNextWord(urlHandler);                               
                break;
            case "Hiragana":               
                urlHandler = 'Hiragana';
                getNextWord(urlHandler);
                break;
            case "Katakana":
                alert('Feature currently unavailable');
                resetGame();
                break;
            case "Korean":
                alert('Feature currently unavailable');
                resetGame();
                break;
        }
    }       
}

function getNextWord(urlHandler) {    
    $.get("/Self_Study/WordQuiz?handler=" + urlHandler, function (data) {             
        displayWordsForGame(data);
    });        
}

function checkGameModeForAnswer(wordToAnswer, answerValue, $selectedButton) { //Checks answer based off game mode
    var urlHandler;
    var payload;
    if (languageFrom === "English") {
        //Translating from english
        switch (languageTo) {
            case "Japanese":               
                urlHandler = "CheckAnswerJapanese";
                payload = { Question: wordToAnswer, Answer: answerValue, TranslateFrom: languageFrom };
                checkAnswer(urlHandler, payload, $selectedButton, wordToAnswer, answerValue);
                break;
            case "Hiragana":
                alert('Feature currently unavailable');
                resetGame();
                break;
            case "Katakana":
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
                urlHandler = "CheckAnswerJapanese";
                payload = { Question: wordToAnswer, Answer: answerValue, TranslateFrom: languageFrom };
                checkAnswer(urlHandler, payload, $selectedButton, wordToAnswer, answerValue);
                break;
            case "Hiragana":
                urlHandler = 'CheckAnswerHiragana';
                payload = { Question: wordToAnswer, Answer: answerValue, TranslateFrom: languageFrom };
                checkAnswer(urlHandler, payload, $selectedButton, wordToAnswer, answerValue);
                break;
            case "Katakana":
                alert('Feature currently unavailable');
                resetGame();
                break;
            case "Korean":
                alert('Feature currently unavailable');
                resetGame();
                break;
        }
    }
}

function checkAnswer(urlHandler, payload, $selectedButton, wordToAnswer, answerValue) {
    $.get("/Self_Study/WordQuiz?handler=" + urlHandler, payload).done(function (result) {           
        var isCorrectObject = JSON.parse(JSON.stringify(result));       

        if (isCorrectObject.answerCheck) {
            answerButtonColorToggle('Correct', $selectedButton);           
            calculateScore(true);            
            recordTimeStamp(wordToAnswer, answerValue);  
           
            checkIfGameEnds(true);                            
        }
        else {
            calculateScore(false);            
            if (isPractice) {               
                answerButtonColorToggle('PracticeIncorrect', $selectedButton);
                disableOrEnableAnswerButtons('Enable');
                disableButton('incorrect', $selectedButton);
            } else {
                answerButtonColorToggle('Incorrect', $selectedButton);
                checkIfGameEnds(false); 
            }                                  
            checkGameStatus();
        }
    });            
}

function checkIfGameEnds(IsCorrectAnswer) {
    $.get("/Self_Study/WordQuiz?handler=CheckGameStatus", {TotalWordCount : totalWordCount}).done(function (result) {
        var IsGameEnd = JSON.parse(JSON.stringify(result));        
        if (IsCorrectAnswer) {          
            if (IsGameEnd.isFinished) {
                setTimeout(function () {
                    gameEndScreen();
                }, timerDelay);       
            } else {
                //Small delay to allow the colour change and to prevent button spam
                setTimeout(function () {                    
                    checkGameModeForNextWord();
                    disableOrEnableAnswerButtons('Enable');
                    checkGameStatus();
                }, timerDelay);
            }               
        } else {           
            if (IsGameEnd.isFinished) {
                setTimeout(function () {
                    gameEndScreen();
                }, timerDelay);                  
            } else {               
                setTimeout(function () {
                    checkGameModeForNextWord();
                    disableOrEnableAnswerButtons('Enable');
                }, timerDelay);    
            }                        
        }        
    });    
}

function displayWordsForGame(words) {
    var randomisedIndex = randomIndex();    
    var displayWord;    
    var selectedWordObject = JSON.parse(JSON.stringify(words)); //Converts JSON into JavaScript object        

    if (words === "Invalid") {
        alert("The token you are passing through is invalid, please log out and log in again to receive a new, valid token");
        scoreOfCorrect = 0;
        scoreOfWrong = 0;
        stopTimer();
        resetTimer();
        resetGame();
        checkGameStatus();  
    } else {
        if (isKana) {
            if (languageFrom === "English") {
                //Translating from english       
                displayWord = selectedWordObject[0].romaji;
                $('.answerButton').each(function (index) {
                    $(this).html(selectedWordObject[randomisedIndex[index]].kana);
                });
            } else {
                //Translating to English            
                displayWord = selectedWordObject[0].kana;
                $('.answerButton').each(function (index) {
                    $(this).html(selectedWordObject[randomisedIndex[index]].romaji);
                });
            }
        } else {
            if (languageFrom === "English") {
                //Translating from english       
                displayWord = selectedWordObject[0].definition;
                $('.answerButton').each(function (index) {
                    $(this).html(selectedWordObject[randomisedIndex[index]].name);
                });
            } else {
                //Translating to English            
                displayWord = selectedWordObject[0].name;
                $('.answerButton').each(function (index) {
                    $(this).html(selectedWordObject[randomisedIndex[index]].definition);
                });
            }
        }

        $('#randomWordText').html(displayWord);
        $('#randomWordText').show();

        refreshGameDisplay();
    }    
}

function randomIndex() {
    var indexArray = [];
    let isExist;
    while (indexArray.length < 4) {
        let rndIndex = Math.trunc(Math.random() * 4);       
        if (indexArray.length === 0) {
            indexArray.push(rndIndex);
        } else {
            for (let i = 0; i < indexArray.length; i++) {
                if (indexArray[i] === rndIndex) {
                    isExist = true;
                    break;
                } else {
                    isExist = false;
                }
            }           

            if (!isExist) {
                indexArray.push(rndIndex);
            }
        }       
    }
    return indexArray;
}

function refreshGameDisplay() {
    //Ensures that the game state does not suddenly change
    resetTimer();
    setTimer();   
    $(".answerButton").each(function () {
        var $selectedButton = $(this);

        if ($selectedButton.hasClass("practiceIncorrect")) {            
            $selectedButton.toggleClass('practiceIncorrect neutral');

        } else if ($selectedButton.hasClass("incorrect")) {
            //$selectedButton.prop("disabled", false);
            setTimeout(function () {
                var $current = $selectedButton;
                $current.toggleClass('incorrect neutral');
            }, timerDelay);

        } else if ($selectedButton.hasClass("correct")) {
            //$selectedButton.prop("disabled", false);
            setTimeout(function () {
                var $current = $selectedButton;
                $current.toggleClass('correct neutral');
            }, timerDelay);
        }
    });   

    $('#gameType').hide();
    $('#game').show();   
}

function disableOrEnableAnswerButtons(whatToDo) {
    if (whatToDo === 'Disable') {
        $('.answerButton').each(function () {
            $(this).prop('disabled', true);
        });
    } else if (whatToDo === 'Enable') {
        $('.answerButton').each(function () {
            $(this).prop('disabled', false);
        });
    }
    
}

function checkGameStatus() {         
    $('#answerScore').html('Correct Answers: ' + scoreOfCorrect + ' | Wrong Answers: ' + scoreOfWrong);

    $('#timerTens').html('00');
    $('#timerSeconds').html('00');
    $('#timerMinutes').html('00');   
}

function gameEndScreen() {

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
    
    $('#timerHistoryField .content').append('<p>' + question + ' => ' + answer + " || " + timeStamp + '</p>');
}