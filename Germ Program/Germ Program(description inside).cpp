

/*
Hassan Harajly
program 3- Germ Associations
3/25/18
the program will have the user enter how many germs they want to input. the first set of input 
starts at germ 1 the user can enter a germ 
that is a child of germ 1 followed by a space and a 0 to end the input for example:
input:
5
0
4 5 1 0
1 0
5 3 0
3 0

this input means:
germ one has no children
germ 2 has 3 children it points to germ 4 5 and 1
germ 3 has 1 child which would be germ 1
germ 4 has 2 children 5 and 3
and germ 5 has 3 as a child
then the program forms a directed acyclic graph data structure and topologically sorts the graph
the topological sort sorts the graph by order of indegrees it sorts the germs with the same indegree lexiographically

corresponding output
output:
2 4 5 3 1
*/

#include <iostream>
#include <string>
#include<vector>
#include <algorithm>
using namespace std;
class priorityqueue
{
private:
	int Size = 0;
	int RightKid = 0;
	int LeftKid = 0;
	vector<int> GermNumbersWithTheSameIndegrees;
public:
	//PreCondition: priority queue is not empty
	//Post Condition deletes the minimum(root) Value of the min heap and replaces the empty root node
	//time o(n)
	//space o(1)
	int deleteTopMinNode()
	{
		int holder = GermNumbersWithTheSameIndegrees.size();
		if (holder == 0)
		{
			return -2;
		}
		GermNumbersWithTheSameIndegrees[0] = GermNumbersWithTheSameIndegrees[holder - 1];
		GermNumbersWithTheSameIndegrees.pop_back();
		DropDown(0);
		return 0;
	}
	//PostCondition:returns the minumum(Root node) germ number
	//time o(1)
	//space o(1)
	int returnminnode()
	{
		return GermNumbersWithTheSameIndegrees[0];
	}
	//precondition:atleast one node in the min heap data structure
	//postcondition:adds germ to correct position in the data structure
	//
	int push(int Germ)
	{
		//bool returns = false;
		int holder = GermNumbersWithTheSameIndegrees.size();
		//(holder == 0) ? (returns = true) : returns = false;
		GermNumbersWithTheSameIndegrees[holder] = Germ;
		SwitchItup(GermNumbersWithTheSameIndegrees.size());
		return 0;
	}

	//precondition: valid germ numbers >=1
	//postcondition: a vector filled with germ numbers representing a min heap tree structure with the root node 
	//being the smallest value

	priorityqueue(vector <int> incomingGermnumbers)
	{
		GermNumbersWithTheSameIndegrees.resize(incomingGermnumbers.size());
		for (int i = 0; i<GermNumbersWithTheSameIndegrees.size(); i++)
			GermNumbersWithTheSameIndegrees[i] = incomingGermnumbers[i];
		FormTree();
	}
	//precondition: positive integer representing a index
	//postcondition: heaps the value up the data structure
	int SwitchItup(int placeHolder2)
	{
		int TopIndex = (placeHolder2 - 1) / 2;
		int SuperficialHolder = 0;
		bool returns = false;
		(placeHolder2 == 0) ? (returns = true) : returns = false;

		if (returns)
		{
			return 0;
		}

		if (GermNumbersWithTheSameIndegrees[placeHolder2] < GermNumbersWithTheSameIndegrees[TopIndex])
		{
			SuperficialHolder = GermNumbersWithTheSameIndegrees[TopIndex];
			//we do the swaparo here
			GermNumbersWithTheSameIndegrees[TopIndex] = GermNumbersWithTheSameIndegrees[placeHolder2];
			GermNumbersWithTheSameIndegrees[placeHolder2] = SuperficialHolder;
			SwitchItup(TopIndex);
		}
		return 0;
	}

	//precondition: positive integer representing an index for the vector(data structure)
	//postcondition: a germ placed in correct position in the data structure

	int DropDown(int PlaceHolder)
	{
		int TemporaryHolder = 0;
		int smallestIndex = PlaceHolder;
		int TempLeftIndex = (2 * PlaceHolder + 1);
		int TempPlace = GermNumbersWithTheSameIndegrees.size();
		int TempRIndex = (2 * PlaceHolder + 2);



		int b = 0;
		if (TempLeftIndex >= TempPlace)
		{
			return 0;
		} 

		
		(GermNumbersWithTheSameIndegrees[PlaceHolder] > GermNumbersWithTheSameIndegrees[TempLeftIndex]) ? (smallestIndex = TempLeftIndex) : b;
		((TempRIndex < TempPlace) && (GermNumbersWithTheSameIndegrees[smallestIndex] > GermNumbersWithTheSameIndegrees[TempRIndex])) ? (smallestIndex = TempRIndex) : b;

		if (smallestIndex != PlaceHolder)
		{
			TemporaryHolder = GermNumbersWithTheSameIndegrees[PlaceHolder];
			GermNumbersWithTheSameIndegrees[PlaceHolder] = GermNumbersWithTheSameIndegrees[smallestIndex];
			GermNumbersWithTheSameIndegrees[smallestIndex] = TemporaryHolder;
			DropDown(smallestIndex);
		}
		return 0;
	}




	//precondition: vector filled with legal data(integer>0)
	//postcondition: places germs in a min heap tree structure
	void FormTree()
	{
		int lengthOfTempTree = GermNumbersWithTheSameIndegrees.size() - 1;
		for (int iterator = lengthOfTempTree; iterator >= 0; iterator--)
		{

			DropDown(iterator);
		}
	}

};

class FormMatrix
{
private:
	int** MatrixPtr;
	vector <int> PointedToNodeCount;
	int numberOfGermsWith0IndgreesDeleted = 0;
	int MatrixSize = 0;
public:
	//precondition:number of germs>0
	//postcondition: 2 dimensional dynamicly allocated array initialized with 0s with the size being number of germ input
	//the 2d array represents a matrix indicating which germ points to which germ and which is pointing to it
	
	FormMatrix(int MatrixSize)
	{
		this->MatrixSize = MatrixSize;
		PointedToNodeCount.resize(MatrixSize);
     	MatrixPtr = new int*[MatrixSize];
	for (int i = 0; i < MatrixSize; i++)
	{
		MatrixPtr[i] = new int[MatrixSize];
	}

	for (int i = 0; i < MatrixSize; i++)
	{
		for (int z = 0; z < MatrixSize; z++)

		{
			MatrixPtr[i][z] = 0;
		}
	}
}
	//precondition:a vector full of germs. and an integer representing the number of germs already removed
	//postcondition:  these germs are to be removed since they were topologically sorted 
void update(vector <int> germstobeupdates, int NumOfGermsDelted)
{
	for (int i = 0; i < germstobeupdates.size(); i++)
	{
		for (int z = 0; z < MatrixSize; z++)
		{
			MatrixPtr[z][germstobeupdates[i]] = -1;
			MatrixPtr[germstobeupdates[i]][z] = -1;
		}
	}
	topilogicalSort(NumOfGermsDelted);



}
//precondition:integer >0 representing a germ parent that will be added and a vector of germ
//children that are pointed to by the parent numbers integers >0
//postcondition: the matrix 2d array is filled with ones so if MatrixPtr[0][1]=1 that means germ 1 points to germ 2
void AddNewVertex(int GermParent, vector<int>GermChildren)
{

	for (int i = 0; i < GermChildren.size(); i++)
	{
		MatrixPtr[GermParent][GermChildren[i] - 1] = 1;

	}
}
//precondition:an integer to keep track of the number of germs that have been topologically sorted
//postcondition:vector that denotes wheater a germ is pointed to or not
int topilogicalSort(int numberOfGermsDeleted)
{
	if (numberOfGermsDeleted == MatrixSize)
	{
		return 0;
	}
	for (int i = 0; i < PointedToNodeCount.size(); i++)
	{
	if (PointedToNodeCount[i] != -1)
	{
	PointedToNodeCount[i] = 0;
	}
	}
	



	for (int i = 0; i < MatrixSize; i++)
	{
		//	cout << "germ #:" << i + 1 << " ";
		for (int z = 0; z < MatrixSize; z++)
		{
			//	cout << MatrixPtr[i][z];
			if (MatrixPtr[i][z] == 1)
			{
				PointedToNodeCount[z] += 1;//at 0 germ 1 being pointed to 
			

			}

		}

	}
	Tied(PointedToNodeCount);
	return 0;


}
//precondition: a vector full of integers representing wheather or not a germ is pointed to
//postcondition: topologically sorts and outputs the germs in the correct order after placing them into a min heap
//and then sorting them lexiographically if they are tied by the number of indegrees
//indegrees= the number of parents a certain germ has
int Tied(vector<int> &pointedTo)
{
	
	int SmallestPointingOutOfGerm = 1000044;
	vector<int> germNumberWithSameInDegrees;
	vector <vector<int>> vect;
	vect.resize(MatrixSize);

	for (int i = 0; i < MatrixSize; i++)
	{
		if (SmallestPointingOutOfGerm > pointedTo[i] && pointedTo[i] != -1)
		{
			SmallestPointingOutOfGerm = pointedTo[i];
		}
		if (pointedTo[i] == 0)
		{


			//cyclicgraph = false;
			vect[i].push_back(i);
			germNumberWithSameInDegrees.push_back(i);
			pointedTo[i] = -1;
			//pointingout[i] = -1;
			numberOfGermsWith0IndgreesDeleted++;

		}
	}

	for (int i = 0; i < vect.size(); i++)
	{
		priorityqueue minheap(vect[i]);
		for (int b = 0; b < vect[i].size(); b++)
		{
			cout << minheap.returnminnode() + 1 << " ";
			minheap.deleteTopMinNode();
		}
	}
	update(germNumberWithSameInDegrees, numberOfGermsWith0IndgreesDeleted);
	return 0;
}
};



//postcondition:calls the graph class constructor, to form a matrix, and topologically sorts it via the topological sort function
int main()
{
	string brokenUpString;
	string germtype, delimiter = " ";
	int numberofgerms = 0;
	size_t startPositionofString = 0;
	cin >> numberofgerms;
	cin.ignore(1000, '\n');
	FormMatrix GraphClass(numberofgerms);


	for (int i = 0; i < numberofgerms; i++)
	{
		vector<int> tokenizedGermNumbers;
		int TempNumHolder = 0;
		getline(cin, germtype);
		while ((startPositionofString = germtype.find(delimiter)) != string::npos) {
			brokenUpString = germtype.substr(0, startPositionofString);
			//	cout << brokenUpString << endl;
			tokenizedGermNumbers.push_back(stoi(brokenUpString));
			germtype.erase(0, startPositionofString + delimiter.length());
		}
		if (stoi(germtype) != 0)
		{
			tokenizedGermNumbers.push_back(stoi(germtype));
		}
		GraphClass.AddNewVertex(i, tokenizedGermNumbers);
	}
	GraphClass.topilogicalSort(0);
	system("pause");
	return 0;
}
