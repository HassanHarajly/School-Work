//hassan harajly
//cis 200
//10/1/2017
//this program is an attempt to make a class like the string class used in c++, it basically loads a character array and modifies its values easily through functions

#include <iostream>
#include <string>

using namespace std;
//precondition: constructor requires a character array to set to the stringarray
//post condition:forming a mystring object allows user to manipulate the character array easily.

class myString {
private:
	char stringarray[27] = "jsjsjsjsjssjsjsjsjsjsjj";
	int i, status = 0, z, startPos;
public:
	myString();
	int setstring(string);//assigns character array parameter to the character array inside mystring class.
	int printstring();//prints the mystring object(the character array inside of object)
	char *getstring();//returns the character array as a char pointer its still somewhat useable as an array and is used inside other functions as so
	int initString();//resets character array to null
	int compareString(myString);//compares both strings to see if theyre different or the same.
	int size();//returns the size of the array
	int replWholeString(myString);//replaces current string with parameter string
	int replPartString(myString, int);//replaces current string with string given at a specific position
	myString partString(int, int);// returns a certain range of the current string
								  //int setstring(char[27], int*);//
	int addEnd(myString);//adds string to the end of current string
	int addStart(myString);//adds string to the begining of current string
	void errorfunction();
};
void myString::errorfunction()
{
	if (status == -1)
	{
		cout << endl << "input error redo the entry you just attempted" << endl << endl;;
		status = 0;
	}
}
//precondition: mystring object with loaded array
//post condition: modified mystring object with new parameter starting off the modified array
int myString::addStart(myString inputstring)
{

	int z = size() - 1;
	if ((size() + inputstring.size()) <= 25)
	{
		for (int e = 0; e < inputstring.size(); e++)
		{
			for (int i = 0; i < size(); i++)//moves string up according to the length of the input string.
			{


				stringarray[z + 1] = stringarray[z];//sets the string to a position forward to make space for new incoming string
				z--;
			}
			z = (size() - 1) + 1;
		}
	}
	else
	{

		status = -1;
		errorfunction();
		return -1;
	}
	if ((size() + inputstring.size()) <= 25)
	{
		for (int i = 0; i < inputstring.size(); i++)
		{
			stringarray[i] = inputstring.getstring()[i];//this sets the old position of the string to a new string
		}
		return 0;
	}
	else
	{

		status = -1;
		errorfunction();
		return -1;
	}


}
//precondition:mystring object with loaded array
//postcondition:currentstring is appended with new string
int myString::addEnd(myString inputstring)
{
	if ((size() + inputstring.size()) <= 25)//will not allow user to enter more than a total of 25 characters to be combined
	{
		i = size();
		for (int q = 0; q < inputstring.size(); q++)//this sets the current strings empty positions with a new string
		{
			stringarray[i] = inputstring.getstring()[q];
			i++;


		}
		return 0;
	}
	else
	{

		status = -1;
		errorfunction();
		return -1;
	}

}
//precondition: starting position of desired return string, and length of the total return value
//postcondition: return a snippet of the current string chosen by the input parameter
myString myString::partString(int startPos, int length)
{
	char temp[27];
	if ((length <= 25 && length >= 1) && (startPos >= 1 && startPos <= 24))
	{


		int t;
		for (t = 0; t < length; t++)
		{
			temp[t] = stringarray[startPos - 1];//sets new array equal to desired return value
			startPos++;
			//cout << t<<endl;
		}
		temp[t] = NULL;
		//cout << temp << endl;
		myString tempstring;//sets array equal to new string
		tempstring.setstring(temp);

		return tempstring;//returns the object to be used
	}
	else
	{
		status = -1;
	}
	errorfunction();

}
//precondition: mystring object,the position of where the function should start replacing the old string with the newstring.
//postcondition: replaced string is returned
//desc:replaces a part of the current string
int myString::replPartString(myString repstring, int startPos)
{
	int t = 0;
	if (startPos <= 1 && startPos >= 25)
	{
		status = -1;
		errorfunction();//start position for string must be between 1 and 25

		return -1;
	}
	else

		for (t = 0; t <repstring.size(); )//loops for how ever big the input string size is
		{
			stringarray[startPos - 1] = repstring.getstring()[t];//sets new string equal to input string
			startPos++;
			t++;
		}

	return 0;
}

//precondition:loaded mystring object
//postcondition:new inputed string
//description: empties current string and inputs new string
int myString::replWholeString(myString input)
{
	//char inputarray[25] = input.getstring;
	if (size() <= 25)
	{
		i = 0;
		while (stringarray[i] != NULL)//empties current string
		{

			stringarray[i] = 0;
			i++;
		}
		i = 0;
		for (int y = 0; y < input.size(); y++)//reloads array with new input
		{
			stringarray[i] = input.getstring()[i];
			i++;
		}
		return 0;
	}
	else
	{

		status = -1;
		errorfunction();
		return -1;
	}


}
//precondition: loaded array
//postcondition returns size of the array

int myString::size()
{
	i = 0;
	while (stringarray[i] != '\0')
	{

		i++;//increments accoring to number of elements of array
	}
	errorfunction();
	return i;

}
//precondition: input string
//postcondition: returns if the strings are similar or different
int myString::compareString(myString inputstring)
{
	bool same = true;
	if (inputstring.size() > 25 || size() > 25)
	{
		status = -1;
	}
	if (inputstring.size() == size())//if the arrays are equal it will evaluate the characters
	{
		for (int p = 0; p < inputstring.size(); p++)
		{
			if ((int(inputstring.getstring()[p])) < (int(getstring()[p])))//will loop until a difference is found among the characters
			{

				//inputstring.printstring();
				same = false;
			}
			if ((int(inputstring.getstring()[p])) > int((getstring()[p])))
			{

				same = false;
				//printstring();
			}
		}
		if (same == true)//if same is true that means the strings are the same and vice versa
		{
			cout << "strings are the same" << endl;
		}
		if (same == false)
		{
			cout << "strings are different" << endl;
		}
	}
	else
	{
		cout << "strings are different" << endl;
	}


	errorfunction();
	return 0;
}
//precondition: loaded char array
//postcondition:resets exisiting string

int myString::initString()
{
	i = 0;

	if (stringarray[0] == NULL)
	{
		status = -1;//if the first position is already null will  exit
		return -1;
	}
	while (stringarray[i] != NULL)//resets the string to null
	{

		stringarray[i] = NULL;


		i++;
	}
	errorfunction();
	return 0;
}
//precondition:loaded array
//postcondition: char array
//returns the char array as a pointer
char *myString::getstring()//returns a char pointer which is sort of what a char array is because we cant return a array in c++ we do it like this
{
	if (size() <= 25)
	{
		return  stringarray;//returns the string if the size is smaller than 25
	}
	else
	{
		errorfunction();
		status = -1;
	}

}
//precondition:loaded array
//post condition:print array contents to screen

int myString::printstring()
{
	//cout << size();
	i = 0;
	if (stringarray[0] == NULL)//will not print if the string is empty or null
	{
		status = -1;
		errorfunction();
		return -1;
	}
	while (stringarray[i] != NULL)
	{

		cout << stringarray[i];//prints all the elements of string
		i++;
	}

	errorfunction();
	return 0;
}
//constructor uses setstring function to initialize the char array
myString::myString()
{


}
//precondition: string from main or anywhere else
//post condition: loaded and validated array
int myString::setstring(string input)
{
	if (input.size() <= 25)//doesnt accept size greater than 25
	{

		i = 0;
		while (stringarray[i] != '\0')
		{
			//will reinitialize string to null
			stringarray[i] = NULL;
			i++;
		}
		i = 0;
		while (stringarray[i] != '\0')
		{

			stringarray[i] = 0;//also reinitilizes array just incase
			i++;
		}
		i = 0;
		while (input[i] != NULL)
		{
			stringarray[i] = input[i];//sets the string here
			i++;
		}
	}
	else {
		status = -1;
		errorfunction();
		return -1;
	}
	//errorfunction();
}

int main()
{
	cout << "welcome to string master 5000! click enter to begin" << endl;
	cin.ignore(100, '\n');
	myString newstring;
	int input = 0;
	string  stringinput, st;
	cout << "enter your new string input" << endl;
	getline(cin, stringinput);//cannot use cin here because it doesnt pick up white spaces so we use cin.get line

	newstring.setstring(stringinput);//sets the string to the classes char array

	for (int i = 0; i < 1;)//will continually loop the menu for the user to select from for the current string 
	{

		cout << "enter new string enter 1" << endl;
		cout << "print your custom string enter 2" << endl;
		cout << "reset your string enter 3" << endl;
		cout << "to compare a new string and current string enter 4" << endl;
		cout << "to replace your whole string with a new one enter 5" << endl;
		cout << "to replace only part of your string enter 6" << endl;
		cout << "to return only part of your string enter 7" << endl;
		cout << "to append a string to your current string enter 8" << endl;
		cout << "to add a string to the front of a existing string enter 9" << endl;
		cout << "to find the size of your string enter 10" << endl;
		cout << "enter 0 to exit" << endl;
		cin >> input;
		while (cin.fail() || !(input >= 0 && input <= 10))//validates input is 1->10
		{
			cout << "error please reenter an integer between 0 and 10";
			cin.clear();
			cin.ignore(100, '\n');
			cin >> input;
		}
		if (input == 1)//chained decision structure to determine which function the user wants to use
		{
			cin.ignore(100, '\n');
			cout << endl << "enter your new string input" << endl;
			getline(cin, stringinput);
			newstring.setstring(stringinput);
		}
		else if (input == 2)//use else if in order to not make computer contiue checking the parameters if one is found to be true
		{
			cin.ignore(100, '\n');
			int num = 0;
			string h;
			cout << endl; newstring.printstring(); cout << endl;
			cout << "enter any input to continue" << endl;
			cin >> h;

		}
		else if (input == 3)
		{
			cin.ignore(100, '\n');
			newstring.initString();
			cout << "your string has been reset" << endl;
		}
		else if (input == 4)
		{

			cin.ignore(199, '\n');
			cout << "enter new string to compare to your current string" << endl;

			getline(cin, st);

			myString input;
			input.setstring(st);
			newstring.compareString(input);
			cout << "enter 1 to continue" << endl;

		}
		else if (input == 5)
		{
			cin.ignore(100, '\n');
			myString into;

			string inp;
			cout << "enter a new string to replace your current one" << endl;
			getline(cin, inp);
			into.setstring(inp);
			newstring.replWholeString(into);
		}
		else if (input == 6)
		{
			cin.ignore(100, '\n');
			string sta;
			int num = 0;
			myString into;

			string inp;
			cout << "enter a new string to replace your current one" << endl;
			cin >> inp;
			into.setstring(inp);
			cout << "enter a starting position to replace your current string with" << endl;
			cin >> num;
			newstring.replPartString(into, num);
			newstring.printstring();
			cout << "enter any input to continue." << endl;
			cin >> sta;

		}
		else if (input == 7)
		{
			cin.ignore(1000, '\n');
			int start = 0;
			int length = 0;
			cout << "Enter the starting position of the current string you want to return" << endl;
			cin >> start;
			while (cin.fail() || !(input >= 1 && input <= 25))//validates input is 1->25
			{
				cout << "error please reenter an integer between 0 and 25";
				cin.clear();
				cin.ignore(100, '\n');
				cin >> input;
			}
			cout << "enter the length of the string you want to return" << endl;
			cin >> length;
			while (cin.fail() || !(input >= 1 && input <= 25))//validates input is 1->25
			{
				cout << "error please reenter an integer between 0 and 25";
				cin.clear();
				cin.ignore(100, '\n');
				cin >> input;
			}
			newstring = newstring.partString(start, length);

			//newstr.printstring();

		}
		else if (input == 8)
		{
			cin.ignore(100, '\n');
			myString strn;
			string strng;
			cout << "Enter the new string you would like to add to the end of your current string" << endl;
			getline(cin, strng);
			strn.setstring(strng);
			newstring.addEnd(strn);
		}
		else if (input == 9)
		{

			cin.ignore(100, '\n');
			myString strn;
			string strng;
			cout << "Enter the string you would like to add to the begining to your current string" << endl;
			getline(cin, strng);
			strn.setstring(strng);
			newstring.addStart(strn);

		}
		else if (input == 10)
		{
			cin.ignore(100, '\n');
			cout << endl << "your string size is " << newstring.size() << endl << endl;

		}
		else if (input == 0)//if user inputs 0 the program will end
		{
			cin.ignore(100, '\n');
			i = 1;
		}

	}


	return 0;
}
