//hassan harajly
//cis 350
//4/13/18
/*
description:
this program is designed to break apart monopolies into smaller companies, this is done
taking into account a few constraints set by the user input, these constraints prevent certain 
subcompanies from merging together.
the input is as follows:
2 integers denoting the number of companies and the number of subcompanies to be formed 
then afterwards the user will enter constraints for every company
so if we enter 
6 3 
3 4 5 0
this means that there is 6 companies we want to break into 3 sub companies
and company number 1 cant combine with company 3 4 and 5.

output will be the result of breaking apart the companies in the desired target of subcompanies
if you cant break it apart in desired number then no solution will be displayed
example:
input
10 3
5 6 2 0
1 7 3 0
2 8 4 0
5 9 3 0
4 10 1 0
1 9 8 0
2 10 9 0
6 10 3 0
4 6 7 0
5 7 8 0
output:
1 3 7
2 4 6 10
5 8 9



*/
#include <iostream>
#include<vector>
#include<string>
using namespace std;
class Dismantle {

private:
	vector<vector<int>> NonMergableCompanies;
	vector <int> potentialCompanies;
	vector<int> temp;
	vector<int> v;
	vector<vector<int>> companies;
public:


	void AddCompanyConstraints(vector<int> Temp)
	{

		NonMergableCompanies.push_back(Temp);

	}

	void destroyCompany(int NumberOfSubCompanies)
	{
		
		
		bool cantcombine = false;
		bool alreadydone=false;
		bool cantcheckagainst = false;
		for (int j = 0; j < NonMergableCompanies.size(); j++)
		{
		
				vector <int> temp2;
				temp2.push_back(j + 1);
				alreadydone = false;
				for (int iterator = 0; iterator < v.size(); iterator++)
				{
					if (j == v[iterator])
					{
						alreadydone = true;
					}
				}
				if (!alreadydone)
				{
					//cout << "here";
					v.push_back(j);
					temp.push_back(j);

					//	cout << j + 1 << " ";
					for (int i = j + 1; i < NonMergableCompanies.size(); i++)
					{
						for (int iterator = 0; iterator < v.size(); iterator++)
						{
							if (i == v[iterator])
							{
								cantcheckagainst = true;
							}
						}
						if (!cantcheckagainst)
						{
							for (int z = 0; z < NonMergableCompanies[i].size(); z++)
							{
								if (j + 1 == NonMergableCompanies[i][z])
								{
									cantcombine = true;
								}
							}
							if (!cantcombine)
							{

								if (backTrack(i) != -1)
								{
									v.push_back(i);
									temp.push_back(i);
									temp2.push_back(i + 1);

									//	cout << ":" << i+1 << " ";
								//	cout << v[j] << " " << endl;
									//	companies.push_back(v);
								}
								else
								{

								}
							}
							cantcombine = false;
						}
						cantcheckagainst = false;
					}
					companies.push_back(temp2);

				}
				alreadydone = false;
				temp.clear();

			
		
		}
		

	}
	int print(int numberofcompanies)
	{
		//cout<<companies.size();
		if (companies.size() == numberofcompanies)
		{
			for (int i = 0; i < companies.size(); i++)
			{
				for (int z = 0; z < companies[i].size(); z++)
				{
					cout << companies[i][z] << " ";
				}
				cout << endl;
			}
		}
		else { cout << "no solution"; }
		return 0;
	}
	int backTrack(int potentialMerger)
	{
		
		for (int i = 0; i < temp.size(); i++)
		{
			for (int j=0;j<NonMergableCompanies[temp[i]].size();j++)
			{
				if (potentialMerger+1 == NonMergableCompanies[temp[i]][j])
				{
					return -1;
				}
			}
		}
	return 0;
	}
};
int main()
{

	string brokenUpString;
	string germtype, delimiter = " ";
	int NumberOfCompanies = 0;
	int TargetSubCompanies = 0;
	size_t startPositionofString = 0;
	cin >> NumberOfCompanies >> TargetSubCompanies;
	//cout << "num of companies" << NumberOfCompanies;
	//	cout << endl<<TargetSubCompanies;
	cin.ignore(1000, '\n');

	Dismantle FormSubcompanies;
	for (int i = 0; i < NumberOfCompanies; i++)
	{
		bool NoConstraintCompany = true;
		vector<int> NonMergableCompanies;
		int TempNumHolder = 0;
		getline(cin, germtype);
		while ((startPositionofString = germtype.find(delimiter)) != string::npos) {
			brokenUpString = germtype.substr(0, startPositionofString);

			NonMergableCompanies.push_back(stoi(brokenUpString));
			germtype.erase(0, startPositionofString + delimiter.length());
			NoConstraintCompany = false;
		}
		if (stoi(germtype) != 0)
		{
			NonMergableCompanies.push_back(stoi(germtype));
		}

		if (NoConstraintCompany)
		{
			NonMergableCompanies.push_back(0);
		}

		FormSubcompanies.AddCompanyConstraints(NonMergableCompanies);

	}
	FormSubcompanies.destroyCompany(TargetSubCompanies);
    FormSubcompanies.print(TargetSubCompanies);
//	system("pause");
	return 0;
}