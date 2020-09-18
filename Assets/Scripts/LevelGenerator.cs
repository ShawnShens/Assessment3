using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject Empty;
    [SerializeField] private GameObject outsideCorner;
    [SerializeField] private GameObject outsideWall;
    [SerializeField] private GameObject insideCorner;
    [SerializeField] private GameObject insideWall;
    [SerializeField] private GameObject standardPellet;
    [SerializeField] private GameObject powerPellet;
    [SerializeField] private GameObject tJunction;

    int[,] fullMap;

    int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };
    

    // Start is called before the first frame update
    void Start()
    {
        fullMap = GetFullMap(levelMap);
        Generator();
    }

    // Update is called once per frame
    void Update()
    {
       
    }




    // Generate the full level
    void Generator()
    {
        float scale = 0.3f;

        for (int i = 28; i >= 0; i--)
        {
            for (int j = 27; j >= 0; j--)
            {
                if (fullMap[i, j] == 1)
                {
                    int rotateOuterCorner = OutsideCornerRotate(i,j);
                    Instantiate(outsideCorner, new Vector3(j * scale, -i * scale, 0.0f), Quaternion.Euler(0, 0, 90 * rotateOuterCorner));
                }
                if (fullMap[i, j] == 2)
                {
                    bool rotateOuterWall = OutsideWallRotate(i, j);
                    if (rotateOuterWall) { Instantiate(outsideWall, new Vector3(j * scale, -i * scale, 0.0f), Quaternion.Euler(0,0,90)); }
                    else { Instantiate(outsideWall, new Vector3(j * scale, -i * scale, 0.0f), Quaternion.identity); }
                }
                if (fullMap[i, j] == 3)
                {
                    int rotateInnerCorner = InsideCornerRotate(i, j);
                    Instantiate(insideCorner, new Vector3(j * scale, -i * scale, 0.0f), Quaternion.Euler(0, 0, 90 * rotateInnerCorner));
                }
                if (fullMap[i, j] == 4)
                {
                    bool rotateInnerWall = OutsideWallRotate(i, j);
                    if (rotateInnerWall) { Instantiate(insideWall, new Vector3(j * scale, -i * scale, 0.0f), Quaternion.Euler(0, 0, 90)); }
                    else { Instantiate(insideWall, new Vector3(j * scale, -i * scale, 0.0f), Quaternion.identity); }
                }
                if (fullMap[i, j] == 5)
                {
                    Instantiate(standardPellet, new Vector3(j * scale, -i * scale, 0.0f), Quaternion.identity);
                }
                if (fullMap[i, j] == 6)
                {
                    Instantiate(powerPellet, new Vector3(j * scale, -i * scale, 0.0f), Quaternion.identity);
                }
                if (fullMap[i, j] == 7)
                {
                    int rotateT = TJunctionRotate(i, j);
                    if (rotateT < 4)
                    {
                        Instantiate(tJunction, new Vector3(j * scale, -i * scale, 0.0f), Quaternion.Euler(0, 0, 90 * rotateT));
                    }
                    else
                    {
                        GameObject temp = Instantiate(tJunction, new Vector3(j * scale, -i * scale, 0.0f), Quaternion.Euler(0, 0, 90 * (rotateT - 4)));
                        Vector3 tempScale = temp.transform.localScale;
                        tempScale.x = -tempScale.x;
                        temp.transform.localScale = tempScale;
                    }             
                }
            }
        }
    }




    // Rotate T Junction
    int TJunctionRotate(int i, int j)
    {
        if (i == 0)
        {
            if (fullMap[i, j - 1] == 2 && fullMap[i + 1, j] == 4) { return 4; }
        }
        else if (i == 28)
        {
            if (fullMap[i, j - 1] == 2 && fullMap[i - 1, j] == 4) { return 2; }
            if (fullMap[i, j + 1] == 2 && fullMap[i - 1, j] == 4) { return 6; }
        }
        // else {} -- In Assessment3, only include 4 T-junctions, this part can be improved in Assessment4 if required

        return 0;
    }




        // Rotate Inner Corner
        int InsideCornerRotate(int i, int j)
    {
        int neighborWall = 0;
        if (fullMap[i, j + 1] == 3 || fullMap[i, j + 1] == 4) { neighborWall += 1; }
        if (fullMap[i, j - 1] == 3 || fullMap[i, j - 1] == 4) { neighborWall += 1; }
        if (fullMap[i + 1, j] == 3 || fullMap[i + 1, j] == 4) { neighborWall += 1; }
        if (fullMap[i - 1, j] == 3 || fullMap[i - 1, j] == 4) { neighborWall += 1; }

        if (neighborWall > 2)
        {
            if (fullMap[i - 1, j + 1] == 0 || fullMap[i - 1, j + 1] == 5) { return 1; }
            if (fullMap[i - 1, j - 1] == 0 || fullMap[i - 1, j - 1] == 5) { return 2; }
            if (fullMap[i + 1, j - 1] == 0 || fullMap[i + 1, j - 1] == 5) { return 3; }
        }
        else
        {
            if ((fullMap[i, j + 1] == 0 || fullMap[i, j + 1] == 5) && (fullMap[i - 1, j] == 0 || fullMap[i - 1, j] == 5)) { return 3; }
            if ((fullMap[i, j + 1] == 0 || fullMap[i, j + 1] == 5) && (fullMap[i + 1, j] == 0 || fullMap[i + 1, j] == 5)) { return 2; }
            if ((fullMap[i, j - 1] == 0 || fullMap[i, j - 1] == 5) && (fullMap[i + 1, j] == 0 || fullMap[i + 1, j] == 5)) { return 1; }
        }

        return 0;
    }




    // Rotate Outside Corner
    int OutsideCornerRotate(int i, int j)
    {
        if (i == 0)
        {
            if (j >1 && j <= 27)
            {
                if (fullMap[i, j - 1] == 2 && fullMap[i + 1, j] == 2) { return 3; }
            }            
        }
        else if (i == 28)
        {
            if (j < 27)
            {
                if (fullMap[i, j + 1] == 2 && fullMap[i - 1, j] == 2) { return 1; }
            }
            if (j > 0)
            {
                if (fullMap[i, j - 1] == 2 && fullMap[i - 1, j] == 2) { return 2; }
            }
            
        }
        else
        {
            if (j > 0)
            {
                if (fullMap[i, j - 1] == 2 && fullMap[i - 1, j] == 2) { return 2; }
                if (fullMap[i, j - 1] == 2 && fullMap[i + 1, j] == 2) { return 3; }
            }
            if (j < 27)
            {
                if (fullMap[i, j + 1] == 2 && fullMap[i - 1, j] == 2) { return 1; }
            }
                 
        }

        return 0;
    }




    // Rotate Outside Wall
    bool OutsideWallRotate(int i, int j)
    {
        if (i == 0)
        {
            if (fullMap[i + 1, j] == 0 || fullMap[i + 1, j] == 5 || fullMap[i + 1, j] == 6)
            {
                return true;
            }
        }
        else if (i == 28)
        {
            if (fullMap[i - 1, j] == 0 || fullMap[i - 1, j] == 5 || fullMap[i - 1, j] == 6)
            {
                return true;
            }
        }
        else
        {
            if (fullMap[i - 1, j] == 0 || fullMap[i - 1, j] == 5 || fullMap[i - 1, j] == 6
                || fullMap[i + 1, j] == 0 || fullMap[i + 1, j] == 5 || fullMap[i + 1, j] == 6)
            {
                return true;
            }
        }
        return false;
    }




    // Generate the full map
    int[,] GetFullMap(int[,] levelMap)
    {
        int[,] Full_levelMap = new int[29,28];

        for (int p = 0; p < 29; p++)
        {
            for (int q = 0; q < 28; q++)
            {
                if (p < 15)
                {
                    if (q < 14) { Full_levelMap[p, q] = levelMap[p, q]; }
                    else { Full_levelMap[p, q] = levelMap[p, 27 - q]; }
                }
                else
                {
                    if (q < 14) { Full_levelMap[p, q] = levelMap[28 - p, q]; }
                    else { Full_levelMap[p, q] = levelMap[28 - p, 27 - q]; }
                }
            }
        }

        return Full_levelMap;
    }
}
