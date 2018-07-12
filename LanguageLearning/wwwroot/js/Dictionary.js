$('#japaneseWordInput').keyup(function () {
    var wordToSearch = $(this).val();
    var kanaEquivalent;
    if (checkInputRegex(wordToSearch)) {
        kanaEquivalent = wanakana.toKana(wordToSearch);
    }
    
    
    //Run GET request for input
    $.get("/Japanese/Dictionary?handler=SearchDictionary", { WordToSearch: wordToSearch, Kana: kanaEquivalent }).done(function (result) {
        var allMatchedWords = JSON.parse(JSON.stringify(result));
        if (allMatchedWords === null || allMatchedWords.length === 0) {
            $('#contentOfWords').html("No matching result");
        } else {
            $('#contentOfWords').empty();
            createTable(allMatchedWords);            
            highlightWords();
            $('#jsonLength').html(JSON.stringify(result));            
        }       
    });
});

$('#contentOfWords').on("click", ".wordInfo", function (event) {
    //alert("hi");
    var kanji = $(this).find('.kanji').text();
    var kana = $(this).find('.kana').text();
    var definition = $(this).find('.definition').text();
    //alert(kana);
    fillModalContent(kanji, kana, definition);
    $("#wordModal").show();
});

$(".close").click(function () {
    $("#wordModal").hide();
});

function createTable(allMatchedWords) {             
    for (var i = 0; i < allMatchedWords.length; i++) {           
        var Name = allMatchedWords[i].name,
            Kana = allMatchedWords[i].kana,
            Definition = allMatchedWords[i].definition,
            POS = allMatchedWords[i].partsOfSpeech,
            Kanji;
            
        if (POS === 'Verb') {
            Definition = "To " + Definition;
        } else {
            Definition = allMatchedWords[i].definition;
        }

        if (Name === Kana) {
            Kanji = Name;
        } else {
            Kanji = Name + '</text>, <text class="words kana">' + Kana;
        }

        var newRowContent = '<tr><td><div class="wordInfo"><text class="words kanji">' + Kanji + '</text><p><text class="words definition">' + Definition + '</text></p></div></td></tr>';
        $("#contentOfWords").append(newRowContent);               
    }   
}

function highlightWords() {   
    var input = $('#japaneseWordInput').val();
    
    var inputKana = wanakana.toKana(input);
    $('.words').each(function () { 
        var result = $(this).html();
        var regex = new RegExp(input, 'gi');
        var highlightedWord = result.replace(regex, function (str) {
            return "<span class='highlight'>" + str + "</span>";
        });
        $(this).html(highlightedWord);
    });      

    if (checkInputRegex(input)) {
        var inputKana = wanakana.toKana(input)
        $('.words').each(function () {
            var result = $(this).html();
            var regex = new RegExp(inputKana, 'gi');
            var highlightedWord = result.replace(regex, function (str) {
                return "<span class='highlight'>" + str + "</span>";
            });
            $(this).html(highlightedWord);
        });      
    }
   
    $(".highlight").css('background-color', '#eeee00');
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




