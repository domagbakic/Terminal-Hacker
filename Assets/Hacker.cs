using UnityEngine;

public class Hacker : MonoBehaviour {

    //Game configuration data
    const string menuHint = "Type 'menu' if you want to get back!";
    string[] Level1Passwords = { "books", "aisle", "shelf", "password", "font", "borrow" };
    string[] Level2Passwords = { "prisoner", "handcuffs", "holster", "uniform", "arrest" };
    string[] Level3Passwords = { "starfield", "telescope", "environment", "exploration", "astronauts" };

    int level;
    string password;

    enum Screen { MainMenu, Password, Win }
    Screen currentScreen = Screen.MainMenu;

	// Use this for initialization
	void Start ()
    {        
        HomeScreen();
    }   
	
	// Update is called once per frame
	void Update ()
    {
    }
    void HomeScreen() {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Press 1 for a local library");
        Terminal.WriteLine("Press 2 for the police station");
        Terminal.WriteLine("Press 3 for NASA");
        Terminal.WriteLine("Enter your selection:");
    }
    void OnUserInput(string input)
    {
        if (input == "menu") //always can jump to menu
        {
            HomeScreen();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input)
    {
        bool isValidLevel = (input == "1" || input == "2" || input == "3");
        if (isValidLevel)
        {
            level = int.Parse(input);
            AskForPassword();
        }        
        else if (input == "007") //easter egg
        {
            Terminal.ClearScreen();
            Terminal.WriteLine("Hello Mr. Bond!\n" + menuHint);
        }
        else //wrong input
        {
            Terminal.ClearScreen();
            Terminal.WriteLine("Wrong input!\n" + menuHint);
        }
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Enter password, hint (" + password.Anagram() + ")");
        Terminal.WriteLine(menuHint);
    }

    private void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = Level1Passwords[Random.Range(0, Level1Passwords.Length)];
                break;
            case 2:
                password = Level2Passwords[Random.Range(0, Level2Passwords.Length)];
                break;
            case 3:
                password = Level3Passwords[Random.Range(0, Level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();  
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowReward();
        Terminal.WriteLine(menuHint);
    }

    void ShowReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    ________
   /       //
  /       //
 /       //
(_______(/
                ");
                break;
            case 2:
                Terminal.WriteLine("Have a police car...");
                Terminal.WriteLine(@"
        _____ 
  _____|_____|_____
 |                 |
 |_________________|
     []       []
                ");
                break;
            case 3:
                Terminal.WriteLine("Have a rocket...");
                Terminal.WriteLine(@"
      _
     / \
    /   \
   |     |
   |     |
   |_____|
  /_______\ 
                ");
                break;
            default:
                Debug.LogError("Invalid level reached");
                break;
        }
    }    
}
