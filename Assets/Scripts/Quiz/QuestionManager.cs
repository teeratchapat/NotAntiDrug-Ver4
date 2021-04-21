using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager instance;

    public GameObject questionHolder;
    public GameObject topBar;
    public GameObject summaryHolder;

    public GameObject correctIcon;
    public GameObject incorrectIcon;

    public Text questionNumber;
    public Text questionText;
    public Text choice1_Text;
    public Text choice2_Text;
    public Text choice3_Text;
    public Text choice4_Text;
    public Text timeLeft_Text;

    public Button choice1_Button;
    public Button choice2_Button;
    public Button choice3_Button;
    public Button choice4_Button;

    [Header("summary")]
    public bool isEndQuiz;
    public GameObject InputNameBox;
    public InputField inputName;
    public Button confirmName_Button;
    public Text SummaryScore_Text;


    public bool isInputName = false;
    public string playerName;

    public string nextScene = "NextScene";
    public string mainMenuScene = "MainMenuMenu";

    public Color defaultButtonColor;
    public Color CorrectButtonColor;

    public List<Question> questions = new List<Question>();
    public List<Question> shuffledQuestions = new List<Question>();

    public int questionLevel = 0;

    public Question tempQuestion;
    public string tempString;
    public int keyIndex;
    public string[] choiceArray;

    public int currentQuestion = 0;
    public int questionAmount = 2;

    [SerializeField] private float time = 120f;
    [SerializeField] private float timeToNotice = 30;
    [SerializeField] private float timeLeft;
    private int timeLeftToDisplay;

    public int scoreToAdd = 100;
    public int scoreToRedure = 100;

    public bool isWait;

    [Header("Highscore")]
    public string filepath;
    public bool isHaveInsertHighscore = false;
    public int newHighscoreIndex = 0;
    [SerializeField] public List<HighScore> highScoreList;

    [Header("Load data")]
    [SerializeField] public List<HighScore> highScoreList_Level01;
    [SerializeField] public List<HighScore> highScoreList_Level02;
    [SerializeField] public List<HighScore> highScoreList_Level03;
    [SerializeField] public int unlockToLevel;
 
    private void Start()
    {
        instance = this;

        Time.timeScale = 1;

        if (questionLevel == 0)
        {
            Debug.Log("choose (int)questionLevel 1-3");
        }else if (questionLevel == 1)
        {
            questions.Add(new Question("สารเสพติดประเภทกดประสาทได้แก่อะไรบ้าง?", "ยาบ้า กาว", "เห็ดขี้ควาย แอลเอสดี", "กาว ฝิ่น", "ช่อดอกกัญชา กาว", 3, 100));
            questions.Add(new Question("อาการของคนเสพสารเสพติดประเภทกดประสาทมีอาการอย่างไร?", "ร่าเริง ช่างพูด", "อ่อนเพลีย ฟุ้งซ่าน", "หงุดหงิด คลุ้มคลั่ง", "ถูกทุกข้อ", 2, 100));
            questions.Add(new Question("กาว และเหล้าเป็นสารเสพติดออกฤทธิ์อย่างไร?", "กดประสาท", "กระตุ้นประสาท", "ผสมผสาน", "หลอนประสาท", 1, 100));
            questions.Add(new Question("กาว ฝิ่น ยาบ้า อะไรไม่ใช่สารเสพติดประเภทกดประสาท?", "กาว", "ฝิ่น", "ไม่ใช่ทั้ง ก และ ข", "ไม่มีข้อถูก", 4, 100));
            questions.Add(new Question("เฮโรอีน จัดเป็นสารเสพติดที่สอดคล้องกับข้อใด?", "เป็นสารเสพติดประเภทที่ 1", "เป็นสารเสพติดประเภทที่ 2", "เป็นสารเสพติดประเภทที่ 3", "เป็นสารเสพติดประเภทที่ 4", 1, 100));
            questions.Add(new Question("ฝิ่นออกฤทธิ์ต่อร่างกายอย่างไร?", "กดประสาท", "กระตุ้นประสาท", "ผสมผสาน", "หลอนประสาท", 1, 100));
        }else if(questionLevel == 2)
        {
            questions.Add(new Question("สารเสพติดประเภทกระตุ้นประสาทได้แก่อะไรบ้าง?", "ยาบ้า กาว", "กาว ฝิ่น", "ยาบ้า ยาอี", "ช่อดอกกัญชา กาว",3,100));
            questions.Add(new Question("อาการของคนเสพสารเสพติดประเภทกระตุ้นประสาทมีอาการอย่างไร?", "ร่าเริง ช่างพูด", "อ่อนเพลีย ฟุ้งซ่าน", "หงุดหงิด กระวนกระวาย", "ถูกทุกข้อ",3,100));
            questions.Add(new Question("ใบกระท่อม ยาบ้าและยาอีเป็นสารเสพติดออกฤทธิ์อย่างไร?", "กดประสาท", "กระตุ้นประสาท", "ผสมผสาน", "หลอนประสาท",2,100));
            questions.Add(new Question("โคเคน ฝิ่น ยาบ้า อะไรไม่ใช่สารเสพติดประเภทกระตุ้นประสาท?", "โคเคน", "ฝิ่น", "ไม่ใช่ทั้ง โคเคน และ ฝิ่น", "ไม่มีข้อถูก",2,100));
            questions.Add(new Question("โคเคนออกฤทธิ์ต่อร่างกายอย่างไร?", "กระตุ้นประสาท", "กดประสาท", "ผสมผสาน", "หลอนประสาท",1,100));
            questions.Add(new Question("ยาบ้า ยาอีจัดเป็นสารเสพติดที่สอดคล้องกับข้อใด?", "เป็นสารเสพติดประเภทที่ 1", "เป็นสารเสพติดประเภทที่ 2", "เป็นสารเสพติดประเภทที3", "เป็นสารเสพติดประเภทที่ 4",2,100));
        } 
        else if(questionLevel == 3)
        {
            questions.Add(new Question("เห็ดขี้ควายเป็นสารเสพติดที่ให้โทษ อย่างไร?", "กดประสาท", "กระตุ้นประสาท", "ผสมผสาน", "หลอนประสาท",4,100));
            questions.Add(new Question("อาการของคนเสพสารเสพติดประเภทหลอนประสาทมีอาการอย่างไร?", "อารมณ์ดี ยิ้มแย่มสดใส", "หูแว่ว เห็นภาพหลอน", "ร่าเริง ช่างพูด", "ถูกทุกข้อ",2,100));
            questions.Add(new Question("อาการของคนเสพสารเสพติดประเภทออกฤทธิ์ผสมผสานมีอาการอย่างไร?", "ร่าเริง ช่างพูด", "หัวเราะตลอดเวลา", "ง่วงนอน เซื่องซึม", "ถูกทุกข้อ",4,100));
            questions.Add(new Question("ในเกมกระสุนประเภทออกฤทธิ์ผสมผสานใช้จัดการกับศัตรูอะไร?", "ช่อดอกกัญชา", "เหล้า", "ฝิ่น", "กาว",1,100));
            questions.Add(new Question("สารเสพติดประเภทหลอนประสาทได้แก่อะไรบ้าง?", "เห็ดขี้ควาย กาว", "เห็ดขี้ควาย แอลเอสดี", "กาว ฝิ่น", "ช่อดอกกัญชา เห็ดขี้ควาย",2,100));
            questions.Add(new Question("วิธีที่ดีที่สุดที่จะไม่ตกเป็นทาสยาเสพติดคือข้อใด?", "การป้องกันชุมชน,การป้องกันสังคม", "การป้องกันสังคม,การป้องกันตัวเอง", "การป้องกันชุมชน,การป้องกันสังคม,การป้องกันตนเอง", "การป้องกันชุมชน,การป้องกันตนเอง,การป้องกันครอบครัว",4,100));
            questions.Add(new Question("เห็ดขี้ควาย จัดเป็นสารเสพติดที่สอดคล้องกับข้อใด?", "เป็นสารเสพติดประเภทที่ 1", "เป็นสารเสพติดประเภทที่ 2", "เป็นสารเสพติดประเภทที่ 3", "เป็นสารเสพติดประเภทที่ 4",3,100));
            questions.Add(new Question("ช่อดอกกัญชา กับเห็ดขี้ควายเป็นสารเสพติดออกฤทธิ์ประเภทเดียวกันหรือไม่?", "เป็นสารเสพติดประเภทเดียวกัน", "เป็นสารเสพติดคนละประเภท", "เห็ดขี้ควายไม่ใช่สารเสพติด", "ไม่มีข้อถูก",2,100));
        }

        for (int i = 0; i < questions.Count; i++)
        {
            int rnd = Random.Range(i, questions.Count);
            tempQuestion = questions[rnd];
            questions[rnd] = questions[i];
            questions[i] = tempQuestion;
        }

        defaultButtonColor = choice1_Button.GetComponent<Image>().color;

        timeLeft = (int)time;

        nextQuestion();

        LoadHighScore();

    }

    private void Update()
    {
        if (!isEndQuiz)
        {
            if (isWait)
            {
                choice1_Button.enabled = false;
                choice2_Button.enabled = false;
                choice3_Button.enabled = false;
                choice4_Button.enabled = false;

                if (Input.anyKeyDown)
                {
                    currentQuestion++;

                    correctIcon.SetActive(false);
                    incorrectIcon.SetActive(false);

                    nextQuestion();

                    choice1_Button.enabled = true;
                    choice2_Button.enabled = true;
                    choice3_Button.enabled = true;
                    choice4_Button.enabled = true;
                }
            }
        }
        else
        {

        }

    }

    private void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;

        timeLeftToDisplay = (int)timeLeft;

        timeLeft_Text.text = timeLeftToDisplay.ToString();

        if (timeLeft <= 0)
        {
            EndQuiz();
        }
        else if (timeLeft <= timeToNotice)
        {
            AudioManager.instance.PlaySfx(17);
        }
    }

    public void nextQuestion()
    {
        if (currentQuestion < questionAmount)
        {
            isWait = false;

            choiceArray = new string[] { questions[currentQuestion].choice1, questions[currentQuestion].choice2, questions[currentQuestion].choice3, questions[currentQuestion].choice4 };
            keyIndex = questions[currentQuestion].key - 1;

            for (int i = 0; i < choiceArray.Length - 1; i++)
            {
                int rnd = Random.Range(i, choiceArray.Length);
                tempString = choiceArray[rnd];
                choiceArray[rnd] = choiceArray[i];
                choiceArray[i] = tempString;

                if (rnd == keyIndex)
                {
                    keyIndex = i;
                }
                else if (i == keyIndex)
                {
                    keyIndex = rnd;
                }
            }

            questionNumber.text = (currentQuestion + 1).ToString();

            choice1_Button.GetComponent<Image>().color = defaultButtonColor;
            choice2_Button.GetComponent<Image>().color = defaultButtonColor;
            choice3_Button.GetComponent<Image>().color = defaultButtonColor;
            choice4_Button.GetComponent<Image>().color = defaultButtonColor;

            questionText.text = questions[currentQuestion].question.ToString();
            choice1_Text.text = choiceArray[0].ToString();
            choice2_Text.text = choiceArray[1].ToString();
            choice3_Text.text = choiceArray[2].ToString();
            choice4_Text.text = choiceArray[3].ToString();
        }
        else
        {
            EndQuiz();
            Debug.Log("EndQuiz");
        }
    }

    public void answerChoice1()
    {
        checkAnswer(0);
    }

    public void answerChoice2()
    {
        checkAnswer(1);
    }

    public void answerChoice3()
    {
        checkAnswer(2);
    }

    public void answerChoice4()
    {
        checkAnswer(3);
    }

    public void checkAnswer(int answer)
    {
        if (answer == keyIndex)
        {
            Debug.Log("correct answer");
            AudioManager.instance.PlaySfx(10);
            //show icon
            correctIcon.SetActive(true);
            //add score
            FindObjectOfType<scoreManager>().addScore(scoreToAdd);

            switch (keyIndex)
            {
                case 0:
                    choice1_Button.GetComponent<Image>().color = CorrectButtonColor;
                    break;
                case 1:
                    choice2_Button.GetComponent<Image>().color = CorrectButtonColor;
                    break;
                case 2:
                    choice3_Button.GetComponent<Image>().color = CorrectButtonColor;
                    break;
                case 3:
                    choice4_Button.GetComponent<Image>().color = CorrectButtonColor;
                    break;
            }

            isWait = true;
        }
        else
        {
            //redure score
            FindObjectOfType<scoreManager>().reduceScore(scoreToRedure);
            AudioManager.instance.PlaySfx(11);
            //show icon
            incorrectIcon.SetActive(true);
            //show right answer
            switch (keyIndex)
            {
                case 0:
                    choice1_Button.GetComponent<Image>().color = CorrectButtonColor;
                    break;
                case 1:
                    choice2_Button.GetComponent<Image>().color = CorrectButtonColor;
                    break;
                case 2:
                    choice3_Button.GetComponent<Image>().color = CorrectButtonColor;
                    break;
                case 3:
                    choice3_Button.GetComponent<Image>().color = CorrectButtonColor;
                    break;
            }
            //wait player click button or press any key then go to nextquestion
            isWait = true;
        }
    }

    public void EndQuiz()
    {
        isEndQuiz = true;
        //deactive question ui and active summary ui
        questionHolder.SetActive(false);
        topBar.SetActive(false);
        summaryHolder.SetActive(true);

        //check highscore
        if(highScoreList == null)
        {
            InputNameBox.SetActive(true);
        }
        else if (highScoreList.Count < 5)
        {
            InputNameBox.SetActive(true);
        }
        else
        {
            for (int i = 0; i < highScoreList.Count; i++)
            {
                if (highScoreList[i].score <= scoreManager.currentScore)
                {
                    InputNameBox.SetActive(true);
                }
            }
        }

        //save without add new score
        SaveGameData();

    }

    public void loadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void SummitName()
    {
        if (inputName.text != null)
        {
            playerName = inputName.text;
        }

        InputNameBox.SetActive(false);
        addNewHighscore();
    }

    public void LoadHighScore()
    {
        if (File.Exists(Application.dataPath + filepath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            //FileStream fileStream = File.Open(Application.persistentDataPath + "/data.text", FileMode.Open);
            FileStream fileStream = File.Open(Application.dataPath + filepath, FileMode.Open);

            //Save save = binaryFormatter.Deserialize(fileStream) as Save;
           GameDataSave gameDataSave = binaryFormatter.Deserialize(fileStream) as GameDataSave;

            fileStream.Close();

            highScoreList_Level01 = gameDataSave.highScoreSaveList_level01;
            highScoreList_Level02 = gameDataSave.highScoreSaveList_level02;
            highScoreList_Level03 = gameDataSave.highScoreSaveList_level03;
            unlockToLevel = gameDataSave.unlockToLevel;

            if (questionLevel == 1)
            {
                highScoreList= highScoreList_Level01;
            }
            else if (questionLevel == 2)
            {
                highScoreList = highScoreList_Level02;
            }
            else if (questionLevel == 3)
            {
                highScoreList = highScoreList_Level03;
            }
        }
        else
        {
            Debug.Log("LoadHighScore not found data");
        }
    }

    public void SaveGameData()
    {
        unlockToLevel = questionLevel + 1;
        GameDataSave gameDataSave = new GameDataSave();
        gameDataSave.highScoreSaveList_level01 = highScoreList_Level01;
        gameDataSave.highScoreSaveList_level02 = highScoreList_Level02;
        gameDataSave.highScoreSaveList_level03 = highScoreList_Level03;
        gameDataSave.unlockToLevel = unlockToLevel;

        Debug.Log("HS_1 = " + gameDataSave.highScoreSaveList_level01.Count);

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        //FileStream fileStream = File.Create(Application.persistentDataPath + "/data.text");
        FileStream fileStream = File.Create(Application.dataPath + filepath);

        binaryFormatter.Serialize(fileStream, gameDataSave);

        fileStream.Close();
    }

    public void addNewHighscore()
    {
        if(highScoreList == null)
        {
            highScoreList.Add(new HighScore(playerName, scoreManager.currentScore));
            Debug.Log("HS null");
        }
        else if (highScoreList.Count <5)
        {
            for (int i = 0; i < highScoreList.Count; i++)
            {
                if (highScoreList[i].score >= scoreManager.currentScore)
                {
                    newHighscoreIndex++;
                }
            }
            
            highScoreList.Insert(newHighscoreIndex, new HighScore(playerName, scoreManager.currentScore));
            
            Debug.Log("HS <5");
        }
        else
        {
            for (int i = 0; i < highScoreList.Count; i++)
            {
                if (highScoreList[i].score >= scoreManager.currentScore)
                {
                    newHighscoreIndex++;
                }
            }

            highScoreList.Insert(newHighscoreIndex, new HighScore(playerName, scoreManager.currentScore));
            highScoreList.RemoveAt(highScoreList.Count - 1);

        }

        Debug.Log(highScoreList.Count);

        if (questionLevel == 1)
        {
            highScoreList_Level01 = highScoreList;
        }
        else if (questionLevel == 2)
        {
            highScoreList_Level02 = highScoreList;
        }
        else if (questionLevel == 3)
        {
            highScoreList_Level03 = highScoreList;
        }

        SaveGameData();
    }
}