using LanguageLearning.Enums;
using LanguageLearning.Interfaces;
using LanguageLearning.Models.LanguageQuiz.WordAppearances;
using LanguageLearning.Models.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LanguageLearning.Models.LanguageQuiz
{
    public class WordQuizDbManager : IWordQuizDbManager
    {
        private readonly WordContext _context;

        public WordQuizDbManager(WordContext context)
        {
            _context = context;
        }       

        public async void AddNewSession(QuizSessionModel quizSessionModel)
        {
            try
            {
                _context.QuizSessionModel.Add(quizSessionModel);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("Session cannot be null");
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong!");
            }
        }

        public async void EndSession(UserData user)
        {
            var allSessions = _context.QuizSessionModel;
            QuizSessionModel userSession = allSessions.FirstOrDefault(u => u.CurrentUser == user);
            if(userSession != null)
            {
                _context.QuizSessionModel.Remove(userSession);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Session does not exist");
            }
        }

        public bool IsExistingSession(UserData user)
        {
            var allSessions = _context.QuizSessionModel;
            QuizSessionModel userSession = allSessions.FirstOrDefault(u => u.CurrentUser == user);
            if(userSession == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }        

        public QuizSessionModel GetUsersSession(UserData user)
        {
            var allSessions = _context.QuizSessionModel;
            QuizSessionModel quizSessionModel = allSessions.FirstOrDefault(q => q.CurrentUser == user);

            return quizSessionModel;
        }

        public bool IsDuplicateWord(UsersCurrentWord usersCurrentWord)
        {
            switch (usersCurrentWord.WordLanguage)
            {
                case Language.Japanese:
                    var allJapaneseWords = _context.JapaneseWordAppearances;
                    JapaneseWordAppearance japaneseWordAppearance = allJapaneseWords.FirstOrDefault(jw => jw.JapaneseWord == usersCurrentWord.CurrentJapaneseWord);
                    if (japaneseWordAppearance == null)                    
                        return false;                   
                    else                   
                        return true;                                     

                case Language.Hiragana:
                    var allHiraganas = _context.HiraganaAppearances;
                    HiraganaAppearance hiraganaAppearance = allHiraganas.FirstOrDefault(h => h.Hiragana == usersCurrentWord.CurrentHiragana);
                    if (hiraganaAppearance == null)
                        return false;
                    else
                        return true;

                case Language.Katakana:
                    var allKatakanas = _context.KatakanaAppearances;
                    KatakanaAppearance katakanaAppearance = allKatakanas.FirstOrDefault(k => k.Katakana == usersCurrentWord.CurrentKatakana);
                    if (katakanaAppearance == null)
                        return false;
                    else
                        return true;

                case Language.Korean:
                    var allKoreanWords = _context.KoreanWordAppearances;
                    KoreanWordAppearance koreanWordAppearance = allKoreanWords.FirstOrDefault(kw => kw.KoreanWord == usersCurrentWord.CurrentKoreanWord);
                    if (koreanWordAppearance == null)
                        return false;
                    else
                        return true;

                default:
                    throw new ArgumentNullException("Language is either null or unrecognised");
            }
        }

        public void UpdateWordAppearance(UsersCurrentWord usersCurrentWord)
        {
            switch (usersCurrentWord.WordLanguage)
            {
                case Language.Japanese:
                    List<JapaneseWordAppearance> japaneseWordAppearance = GetAllJapaneseWordAppearances(usersCurrentWord.User);
                    if (!IsExistingJapaneseWord(usersCurrentWord.CurrentJapaneseWord, japaneseWordAppearance))
                    {
                        AddNewWordAppearance(usersCurrentWord);
                    }
                    break;

                case Language.Hiragana:
                    List<HiraganaAppearance> hiraganaAppearance = GetAllHiraganaAppearances(usersCurrentWord.User);
                    if (!IsExistingHiragana(usersCurrentWord.CurrentHiragana, hiraganaAppearance))
                    {
                        AddNewWordAppearance(usersCurrentWord);
                    }
                    break;

                case Language.Katakana:
                    List<KatakanaAppearance> katakanaAppearance = GetAllKatakanaAppearances(usersCurrentWord.User);
                    if (!IsExistingKatakana(usersCurrentWord.CurrentKatakana, katakanaAppearance))
                    {
                        AddNewWordAppearance(usersCurrentWord);
                    }
                    break;

                case Language.Korean:
                    List<KoreanWordAppearance> koreanWordAppearance = GetAllKoreanWordAppearances(usersCurrentWord.User);
                    if (!IsExistingKoreanWord(usersCurrentWord.CurrentKoreanWord, koreanWordAppearance))
                    {
                        AddNewWordAppearance(usersCurrentWord);
                    }
                    break;            
                    
                default:
                    throw new ArgumentNullException("Language is either null or unrecognised");                    
            }                        
        }

        private List<JapaneseWordAppearance> GetAllJapaneseWordAppearances(UserData user)
        {
            IQueryable<JapaneseWordAppearance> JapaneseWordAppearancesQuery = from words in _context.JapaneseWordAppearances
                                                                      select words;
            List<JapaneseWordAppearance> allJapaneseWordAppearances = new List<JapaneseWordAppearance>();

            JapaneseWordAppearancesQuery = JapaneseWordAppearancesQuery.Where(jw => jw.User == user);
            allJapaneseWordAppearances = JapaneseWordAppearancesQuery.ToList();

            return allJapaneseWordAppearances;
        }

        private List<HiraganaAppearance> GetAllHiraganaAppearances(UserData user)
        {            
            IQueryable<HiraganaAppearance> hiraganaAppearancesQuery = from words in _context.HiraganaAppearances
                                                                      select words;
            List<HiraganaAppearance> allHiraganaAppearances = new List<HiraganaAppearance>();

            hiraganaAppearancesQuery = hiraganaAppearancesQuery.Where(h => h.User == user);
            allHiraganaAppearances = hiraganaAppearancesQuery.ToList();
           
            return allHiraganaAppearances;
        }

        private List<KatakanaAppearance> GetAllKatakanaAppearances(UserData user)
        {
            IQueryable<KatakanaAppearance> katakanaAppearancesQuery = from words in _context.KatakanaAppearances
                                                                      select words;
            List<KatakanaAppearance> allKatakanaAppearances = new List<KatakanaAppearance>();

            katakanaAppearancesQuery = katakanaAppearancesQuery.Where(k => k.User == user);
            allKatakanaAppearances = katakanaAppearancesQuery.ToList();

            return allKatakanaAppearances;
        }

        private List<KoreanWordAppearance> GetAllKoreanWordAppearances(UserData user)
        {
            IQueryable<KoreanWordAppearance> koreanWordAppearancesQuery = from words in _context.KoreanWordAppearances
                                                                      select words;
            List<KoreanWordAppearance> allKoreanWordAppearances = new List<KoreanWordAppearance>();

            koreanWordAppearancesQuery = koreanWordAppearancesQuery.Where(kw => kw.User == user);
            allKoreanWordAppearances = koreanWordAppearancesQuery.ToList();

            return allKoreanWordAppearances;
        }

        private bool IsExistingJapaneseWord(JapaneseWord japaneseWord, List<JapaneseWordAppearance> japaneseWordAppearances)
        {
            if (japaneseWordAppearances.Exists(jw => jw.JapaneseWord == japaneseWord))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsExistingHiragana(Hiragana hiragana, List<HiraganaAppearance> hiraganaAppearances)
        {
            if (hiraganaAppearances.Exists(h => h.Hiragana == hiragana))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsExistingKatakana(Katakana katakana, List<KatakanaAppearance> katakanaAppearances)
        {
            if (katakanaAppearances.Exists(k => k.Katakana == katakana))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsExistingKoreanWord(KoreanWord koreanWord, List<KoreanWordAppearance> koreanWordAppearances)
        {
            if (koreanWordAppearances.Exists(kw => kw.KoreanWord == koreanWord))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void AddNewWordAppearance(UsersCurrentWord usersCurrentWord)
        {
            switch (usersCurrentWord.WordLanguage)
            {
                case Language.Japanese:
                    JapaneseWordAppearance japaneseWordAppearance = new JapaneseWordAppearance
                    {
                        User = usersCurrentWord.User,
                        JapaneseWord = usersCurrentWord.CurrentJapaneseWord
                    };
                    _context.JapaneseWordAppearances.Add(japaneseWordAppearance);
                    await _context.SaveChangesAsync();                
                    break;

                case Language.Hiragana:
                    HiraganaAppearance hiraganaAppearance = new HiraganaAppearance
                    {
                        User = usersCurrentWord.User,
                        Hiragana = usersCurrentWord.CurrentHiragana
                    };
                    _context.HiraganaAppearances.Add(hiraganaAppearance);
                    await _context.SaveChangesAsync();
                    break;

                case Language.Katakana:
                    KatakanaAppearance KatakanaAppearance = new KatakanaAppearance
                    {
                        User = usersCurrentWord.User,
                        Katakana = usersCurrentWord.CurrentKatakana
                    };
                    _context.KatakanaAppearances.Add(KatakanaAppearance);
                    await _context.SaveChangesAsync();
                    break;

                case Language.Korean:
                    KoreanWordAppearance koreanWordAppearance = new KoreanWordAppearance
                    {
                        User = usersCurrentWord.User,
                        KoreanWord = usersCurrentWord.CurrentKoreanWord
                    };
                    _context.KoreanWordAppearances.Add(koreanWordAppearance);
                    await _context.SaveChangesAsync();
                    break;

                default:
                    throw new ArgumentNullException("Language is either null or unrecognised");
            }
        }

        public void ResetWordAppearance(UsersCurrentWord usersCurrentWord)
        {
            switch (usersCurrentWord.WordLanguage)
            {
                case Language.Japanese:
                    List<JapaneseWordAppearance> japaneseWordAppearance = GetAllJapaneseWordAppearances(usersCurrentWord.User);
                    DeleteAllJapaneseWordAppearances(japaneseWordAppearance);
                    break;

                case Language.Hiragana:
                    List<HiraganaAppearance> hiraganaAppearance = GetAllHiraganaAppearances(usersCurrentWord.User);
                    DeleteAllHiraganaAppearances(hiraganaAppearance);
                    break;

                case Language.Katakana:
                    List<KatakanaAppearance> katakanaAppearance = GetAllKatakanaAppearances(usersCurrentWord.User);
                    DeleteAllKatakanaAppearances(katakanaAppearance);
                    break;

                case Language.Korean:
                    List<KoreanWordAppearance> koreanWordAppearance = GetAllKoreanWordAppearances(usersCurrentWord.User);
                    DeleteAllKoreanWordAppearances(koreanWordAppearance);
                    break;

                default:
                    throw new ArgumentNullException("Language is either null or unrecognised");
            }
        }

        private async void DeleteAllJapaneseWordAppearances(List<JapaneseWordAppearance> japaneseWordAppearance)
        {
            foreach (JapaneseWordAppearance jWord in japaneseWordAppearance)
            {
                _context.JapaneseWordAppearances.Remove(jWord);
                await _context.SaveChangesAsync();
            }
        }       
        
        private async void DeleteAllHiraganaAppearances(List<HiraganaAppearance> hiraganaAppearance)
        {
            foreach (HiraganaAppearance hiragana in hiraganaAppearance)
            {
                _context.HiraganaAppearances.Remove(hiragana);
                await _context.SaveChangesAsync();
            }
        }

        private async void DeleteAllKatakanaAppearances(List<KatakanaAppearance> katakanaAppearance)
        {
            foreach (KatakanaAppearance katakana in katakanaAppearance)
            {
                _context.KatakanaAppearances.Remove(katakana);
                await _context.SaveChangesAsync();
            }
        }

        private async void DeleteAllKoreanWordAppearances(List<KoreanWordAppearance> koreanWordAppearance)
        {
            foreach (KoreanWordAppearance kWord in koreanWordAppearance)
            {
                _context.KoreanWordAppearances.Remove(kWord);
                await _context.SaveChangesAsync();
            }
        }
    }
}
