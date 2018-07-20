var languageSelection = "";

if (languageSelection === "") {
    $('#userWordInput').prop("disabled", true);
} else {
    $('#userWordInput').prop("disabled", false);
}

$('#userWordInput').keyup(function () {
    var wordToSearch = $(this).val();
    var kanaEquivalent;
    var languageOfInput;
    
    var payload;
    var urlHandler;
    
    switch (languageSelection) {
        case "Japanese":
            if (checkInputRegex(wordToSearch)) {
                kanaEquivalent = wanakana.toKana(wordToSearch);
                languageOfInput = "English";
            } else {
                kanaEquivalent = "";
                languageOfInput = "Japanese";
            }
            
            urlHandler = "SearchJapaneseInput";
            payload = { WordToSearch: wordToSearch, Kana: kanaEquivalent, InputLanguage: languageOfInput };
            break;
        case "Korean":
            break;        
    }

    getWords(payload, urlHandler);   
});

$('.languageButton').click(function () {
    var $selectedButton = $(this);
    var language = $selectedButton.html();

    languageSelection = language;
    resetButtons();
    disableButton('noJQuery', $selectedButton);
    $('#userWordInput').prop("disabled", false);

    switch (language) {
        case "Japanese":
            $('#description').html("Search Japanese or English words using kanji, kana or romaji by typing in the box below");
            break;
        case "Korean":
            $('#description').html("Korean is currently unavailable");
            $('#userWordInput').prop("disabled", true);
            break;
    }
    
});

$('#contentOfWords').on("click", ".wordInfo", function (event) {   
    var kanji = $(this).find('.kanji').text();
    var kana = $(this).find('.kana').text();
    var definition = $(this).find('.definition').text();
    
    fillModalContent(kanji, kana, definition);
    $("#wordModal").show();
});

$(".close").click(function () {
    $("#wordModal").hide();
});

function getWords(data, urlHandler) {
    //Run GET request for input   
    $.get("/Dictionary?handler=" + urlHandler, data).done(function (result) {
        var allMatchedWords = JSON.parse(JSON.stringify(result));
        if (allMatchedWords === null || allMatchedWords.length === 0) {
            $('#contentOfWords').html("No matching result");
        } else {
            $('#contentOfWords').empty();
            createTable(allMatchedWords);
            checkInputForHighlight();
            //$('#jsonLength').html(JSON.stringify(result));
        }
    });
}

function createTable(allMatchedWords) {             
    for (var i = 0; i < allMatchedWords.length; i++) {           
        var Name = allMatchedWords[i].name,
            Kana = allMatchedWords[i].kana,
            Definition = allMatchedWords[i].definition,
            POS = allMatchedWords[i].partsOfSpeech,
            Kanji;
                              
        if (Name === Kana) {            
            Kanji = Name;
        } else {
            Kanji = Name + '</text>, <text class="words kana">' + Kana;
        }

        var newRowContent = '<tr><td><div class="wordInfo"><text class="words kanji">' + Kanji + '</text><p><text class="words definition">' + Definition + '</text></p></div></td></tr>';

        $("#contentOfWords").append(newRowContent);               
    }   
}

function disableButton(classToToggle, $selectedButton) {
    $selectedButton.prop("disabled", true);

    if (classToToggle === 'noJQuery') {
        $selectedButton.toggleClass(classToToggle + ' hover');
    }
}

function resetButtons() {
    $(".languageButton").each(function () {
        if (!$(this).hasClass("hover")) {
            $(this).prop("disabled", false);
            $(this).toggleClass('noJQuery hover');
        }
    });
}

function checkInputForHighlight() {   
    var input = $('#userWordInput').val();   
    var inputKana = wanakana.toKana(input);

    $('.kana').each(function () {
        var whatToHighlight = $(this).html();
        var classIdentity = $(this);

        highlightWords(whatToHighlight, classIdentity, inputKana);
    });

    $('.kanji').each(function () {
        var whatToHighlight = $(this).html();
        var classIdentity = $(this);

        highlightWords(whatToHighlight, classIdentity, inputKana);
    });
  
    $('.definition').each(function () { 
        var whatToHighlight = $(this).html();
        var classIdentity = $(this);

        highlightWords(whatToHighlight, classIdentity, input);
    });      
           
    $(".highlight").css('background-color', '#eeee00');    
}

function highlightWords(whatToHighlight, classIdentity, userInput) {          
    var regex = new RegExp(userInput, 'gi');
    var highlightedWord = whatToHighlight.replace(regex, function (str) {
        return "<span class='highlight'>" + str + "</span>";
    });
    $(classIdentity).html(highlightedWord);      
}

function fillModalContent(kanji, kana, definition) {
    $(".modal-body").empty();

    var title = "<h2>" + kanji + "</h2>";
    var content = "<p><h3>" + kana + "</h3></p><p>Meaning</p><p>" + definition + "</p>";
    
    $(".modal-body").append(title+content);
}

function checkInputRegex(input) {
    var english = /^[A-Za-z0-9]*$/;
    return english.test(input);     
}




