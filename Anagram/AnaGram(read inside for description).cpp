//hassan harajly	
//cis 350
//anagram program
/*description: This program asks the user to enter 2 integers(whole numbers) with a space inbetween them,the first number is how many
words/numbers the user will input. these words/numbers that are being entered will be checked to see if any of the words/numbers are anagrams
so the first number will determine how many strings(groups) of characters and numbers the user will input, then after the space a another number will be input
this number will determine which class you would like displayed, for example if there are 4 words that are anagrams of each other then that is a class 4
it will print out the class you requested and any class underneath so class 4,3,2,1 and so forth.

ACCEPTS:
numbers letters symbols such as & %, also has the ability to differentiate between spaces inside of the input and removes the spaces
*/
#include <iostream>
#include <string>
#include<vector>
#include<algorithm>

using namespace std;
class EquiClasses
{
private:
	vector< vector<string> > classes;
	vector <string> sortedList, templist;
	vector <string> anagramlist;
public:
	//precondition: word= string of characters, spaces, numbers or special characters
	//postcondition: anagram list(a vector) is filled with a list of strings 
	void add(string word)
	{
		anagramlist.push_back(word);



	}
	bool vectorsort(const vector<string>& a, const vector<string>& b)
	{
		return a.size() < b.size();
	}
	//precondition: function doesnt accept a vector as a paramter but if add function is called there is one inside of the class
	//postcondition: anagrams are grouped together in a sorted fashion ,and each "class"(number of same anagrams) of anagrams is sorted if the same number of classes exits
	void sortClasses()
	{
		bool flag = true;
		templist = anagramlist;


		for (int i = 0; i < anagramlist.size(); i++)
		{
			sort(anagramlist[i].begin(), anagramlist[i].end());
		}


		for (int i = 0; i < anagramlist.size(); i++)
		{
			int check = 0;

			vector <string> finallist;

			for (int j = 1; j < anagramlist.size(); j++)
			{
				if ((i) != j && i<j)
				{

					if ((anagramlist[i].compare(anagramlist[j]) == 0) && templist[j].compare(templist[i]) != 0)
					{


						check++;
						finallist.push_back(templist[j]);
						templist[j].clear();

					}

				}

			}
			for (int z = 0; z < classes.size(); z++)
			{
				for (int b = 0; b < classes[z].size(); b++)
				{
					if (templist[i] == classes[z][b])
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				finallist.push_back(templist[i]);
				classes.push_back(finallist);
			}
			flag = true;

		}

		vector<string> vec;
		for (int i = 0; i < classes.size(); i++)
		{
			sort(classes[i].begin(), classes[i].end());
		}
		for (int i = 0; i < classes.size(); i++)
		{
			sort(classes.begin(), classes.end());
		}
		sort(classes.begin(), classes.end(), [](const vector<string> & a, const vector<string> & b) { return a.size() > b.size(); });

	}

	//precondition: takes a integer parameter denoting how many classes the user wishes to display 
	/*post condition: the function will print out the first class of anagrams denoted by the number parameter and prints any classes under it
	for example num2print=5 will print class 5 4 3 2 1 for a maximum of 5 classes only displayed though
	*/
	int printClasses(int num2Print)
	{
		if (num2Print <= 0)
		{
			return 0;
		}
		int j = 0, max = 0, numberofprints = 0;
		for (int i = 0; i < classes.size(); i++)
		{
			if (max < classes[i].size())
			{
				max = classes[i].size();
			}
		}

		while (max != 0)
		{
			for (int i = 0; i < classes.size(); i++)
			{
				j = 0;
				if (classes[i].size() == max)
				{
					for (int z = 0; z < classes[i].size(); z++)
					{
						if (classes[i][z] != "")
						{
							if (z == 0)
							{
								numberofprints++;
								cout << "Class of size " << classes[i].size() << ": ";
							}
							j++;
							cout << classes[i][z] << " ";

						}
					}
					if (j > 0)
					{
						cout << "." << endl;
					}
					if (numberofprints == num2Print)
					{
						return 0;
					}
				}

			}
			max--;
		}
		return 0;
	}
};
//precondition:input from user in the form of "integer integer"
//postcondition:sort and print function calls which arrange anagrams in a certain order before printing them out
int main()
{
	EquiClasses Anagramclass;
	int classNumber = 0, numberofanagrams = 0;
	string dummyString;

	string nums;
	int i = 0, classsize = 0, spacekeylocation = 0;
	int numberofwhitespaces = 0;
	getline(cin, nums);

	for (int i = 0; i < nums.size(); i++)
	{
		if (!(isdigit(nums[i]) || nums[i] == ' '))
		{
			cout << "you must enter 2 numbers seperated by a single space" << endl;
			return 0;

		}
		if (nums[i] == ' ')
		{
			spacekeylocation = i;
			numberofwhitespaces++;
		}
		if ((i + 1) >= nums.size())
		{
			if (numberofwhitespaces == 0 || numberofwhitespaces > 1)
			{
				cout << "you must enter 2 numbers seperated by a single space" << endl;
				return 0;
			}
		}

	}


	for (int i = 0; i < nums.size(); i++)
	{
		char * numtemp = &nums[0];

		numberofanagrams = atoi(numtemp);

		char * numtemp2 = &nums[spacekeylocation];
		classsize = atoi(numtemp2);
	}

	for (int i = 0; i < numberofanagrams; i++)
	{

		getline(cin, dummyString);
		dummyString.erase(std::remove(dummyString.begin(), dummyString.end(), ' '), dummyString.end());

		Anagramclass.add(dummyString);
	}
	Anagramclass.sortClasses();
	Anagramclass.printClasses(classsize);

	return 0;
}