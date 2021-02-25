using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using MT = System.Math;

namespace FunctionNeuralNetwork.Charts
{
    public class SimplyPointCollection
    {
        /// <summary>
        ///  Returns a Pointcollection that keeps only each nth point 
        /// </summary>
        /// <param name="lsPtColl">original PointCollection</param>
        /// <param name="lnRange">nth element to keep</param>
        /// <returns>PointCollection reduced</returns>
        static public PointCollection SimplyNData(PointCollection lsPtColl, int lnRange)
        {
            int lnNData = lsPtColl.Count;
            PointCollection loResult = new PointCollection();
            int lnPivot = lnRange;
            if (lnNData > 0 && lnRange > 0)
            {
                loResult.Add(new Point(lsPtColl[0].X, lsPtColl[0].Y));
                for (int i = 1; i < lnNData - 1; i++)
                {
                    if (i == lnPivot)
                    {
                        loResult.Add(new Point(lsPtColl[i].X, lsPtColl[i].Y));
                        lnPivot += lnRange;
                    }
                }
                loResult.Add(new Point(lsPtColl[lnNData - 1].X, lsPtColl[lnNData - 1].Y));
            }
            else
                loResult = lsPtColl;
            return loResult;
        }

        /// <summary>
        /// Returns a PointCollection that keeps only elementes in a certain distance
        /// </summary>
        /// <param name="lsPtColl">original Pointcollection</param>
        /// <param name="lnEps">distance of reference</param>
        /// <returns>PointCollection reduced</returns>
        static public PointCollection RadialDist(PointCollection lsPtColl, double lnEps)
        {
            int lnNData = lsPtColl.Count;
            PointCollection loResult = new PointCollection();

            if (lnNData > 0 && lnEps > 0)
            {

                loResult.Add(new Point(lsPtColl[0].X, lsPtColl[0].Y));
                int lnPivot = 0;
                for (int i = 1; i < lnNData - 1; i++)
                {
                    double lnDist = Vector2D.Dist2Pts(lsPtColl[lnPivot], lsPtColl[i]);
                    if (lnDist > lnEps)
                    {
                        loResult.Add(new Point(lsPtColl[i].X, lsPtColl[i].Y));
                        lnPivot = i;
                    }
                }
                loResult.Add(new Point(lsPtColl[lnNData - 1].X, lsPtColl[lnNData - 1].Y));
            }
            else
                loResult = lsPtColl;
            return loResult;
        }

        /// <summary>
        /// Returns a PointCollection that keeps only elementes in a certain Perpendicular distance
        /// </summary>
        /// <param name="lsPtColl">original Pointcollection</param>
        /// <param name="lnEps">distance of reference</param>
        /// <returns>PointCollection reduced</returns>
        static public PointCollection PerpendicularDist(PointCollection lsPtColl, double lnEps)
        {

            int lnNData = lsPtColl.Count;
            PointCollection loResult = new PointCollection();

            if (lnNData > 3 && lnEps > 0)
            {
                loResult.Add(new Point(lsPtColl[0].X, lsPtColl[0].Y));
                int lnPivot = 0;
                int lnLineExt = 2;
                for (int i = 1; i < lnNData - 2; i++)
                {
                    double lnDist = DistLine2Point(lsPtColl[lnPivot], lsPtColl[lnLineExt], lsPtColl[i]);
                    if (lnDist > lnEps)
                    {
                        loResult.Add(new Point(lsPtColl[i].X, lsPtColl[i].Y));
                        lnPivot = i;
                        lnLineExt = i + 2;
                    }
                    else
                    {
                        loResult.Add(new Point(lsPtColl[lnLineExt].X, lsPtColl[lnLineExt].Y));
                        lnPivot = lnLineExt;
                        i = lnLineExt;
                        lnLineExt += 2;
                    }
                }
                loResult.Add(new Point(lsPtColl[lnNData - 1].X, lsPtColl[lnNData - 1].Y));
            }
            else
                loResult = lsPtColl;
            return loResult;
        }

        /// <summary>
        ///  Returns a PointCollection that keeps only elements out a strip of reference
        /// </summary>
        /// <param name="lsPtColl">original Pointcollection</param>
        /// <param name="lnEps">distance of reference</param>
        /// <returns>PointCollection reduced</returns>
        static public PointCollection ReumannWitkam(PointCollection lsPtColl, double lnEps)
        {

            int lnNData = lsPtColl.Count;
            PointCollection loResult = new PointCollection();

            if (lnNData > 3 && lnEps > 0)
            {
                loResult.Add(new Point(lsPtColl[0].X, lsPtColl[0].Y));
                int lnPivot = 0;
                int lnLineExt = 1;
                for (int i = 2; i < lnNData; i++)
                {
                    double lnDist = DistLine2Point(lsPtColl[lnPivot], lsPtColl[lnLineExt], lsPtColl[i]);
                    if (lnDist > lnEps)
                    {
                        loResult.Add(new Point(lsPtColl[i - 1].X, lsPtColl[i - 1].Y));
                        lnPivot = i - 1;
                        lnLineExt = i;

                    }
                }
                loResult.Add(new Point(lsPtColl[lnNData - 1].X, lsPtColl[lnNData - 1].Y));
            }
            else
                loResult = lsPtColl;
            return loResult;
        }

        /// <summary>
        ///  Returns a PointCollection that keeps only elementes out a strip of reference
        /// </summary>
        /// <param name="lsPtColl">original Pointcollection</param>
        /// <param name="lnEps">distance of reference</param>
        /// <param name="lnPtLimit">Number of poits to reduce</param>
        /// <returns>PointCollection reduced</returns>
        static public PointCollection ReumannWitkam(PointCollection lsPtColl, double lnEps, int lnPtLimit)
        {

            int lnNData = lsPtColl.Count;
            PointCollection loResult = new PointCollection();

            if (lnNData > 3 && lnEps > 0 && lnPtLimit > 1)
            {
                int lnPivot = 0;
                int lnLineExt = 1;
                int lnBegin = 2;
                for (int k = 0; k < lsPtColl.Count - 1; k++)
                {
                    if (!double.IsNaN(lsPtColl[k].X) && !double.IsNaN(lsPtColl[k].Y) &&
                        !double.IsNaN(lsPtColl[k + 1].X) && !double.IsNaN(lsPtColl[k + 1].Y))
                    {
                        loResult.Add(new Point(lsPtColl[k].X, lsPtColl[k].Y));
                        lnPivot = k;
                        lnLineExt = k + 1;
                        lnBegin = k + 2;
                        break;
                    }
                }

                int lnLimCount = 1;
                for (int i = lnBegin; i < lnNData; i++)
                {
                    double lnDist = DistLine2Point(lsPtColl[lnPivot], lsPtColl[lnLineExt], lsPtColl[i]);
                    if (!double.IsNaN(lnDist) && !double.IsInfinity(lnDist))
                    {
                        if (lnDist > lnEps)
                        {
                            loResult.Add(new Point(lsPtColl[i - 1].X, lsPtColl[i - 1].Y));
                            lnPivot = i - 1;
                            lnLineExt = i;
                            lnLimCount = 1;
                        }
                        else
                        {
                            lnLimCount++;
                            if (lnLimCount > lnPtLimit)
                            {
                                loResult.Add(new Point(lsPtColl[i - 1].X, lsPtColl[i - 1].Y));
                                lnPivot = i - 1;
                                lnLineExt = i;
                                lnLimCount = 1;
                            }
                        }
                    }
                    else
                        lnPivot = i;
                }
                loResult.Add(new Point(lsPtColl[lnNData - 1].X, lsPtColl[lnNData - 1].Y));
            }
            else
                loResult = lsPtColl;
            return loResult;
        }

        static public PointCollection Opheim(PointCollection lsPtColl, double lnPerTol, double lnRadTol)
        {
            int lnNData = lsPtColl.Count;
            PointCollection loResult = new PointCollection();
            if (lnNData > 3 && lnPerTol > 0 && lnRadTol > 0)
            {
                loResult.Add(new Point(lsPtColl[0].X, lsPtColl[0].Y));
                int lnPivot = 0;
                int lnLineExt = 1;

                for (int i = 2; i < lnNData; i++)
                {
                    double lnDistLin = DistLine2Point(lsPtColl[lnPivot], lsPtColl[lnLineExt], lsPtColl[i]);
                    double lnDistRad = Vector2D.Dist2Pts(lsPtColl[lnPivot], lsPtColl[i]);

                    if (lnDistLin > lnPerTol && lnDistRad > lnRadTol)
                    {
                        if (i != 1)
                        {
                            loResult.Add(new Point(lsPtColl[i - 1].X, lsPtColl[i - 1].Y));
                            lnPivot = i - 1;
                            i--;
                        }
                        else
                        {
                            loResult.Add(new Point(lsPtColl[i].X, lsPtColl[i].Y));
                            lnPivot = i;
                        }
                    }
                }

                loResult.Add(new Point(lsPtColl[lnNData - 1].X, lsPtColl[lnNData - 1].Y));
            }
            else
                loResult = lsPtColl;
            return loResult;

        }


        static public PointCollection LangSimply(PointCollection lsPtColl, int lnIntv, double lnEsp)
        {
            int lnNData = lsPtColl.Count;
            PointCollection loResult = new PointCollection();
            if (lnNData > 3 && lnIntv >= 2 && lnIntv < lnNData && lnEsp > 0)
            {
                loResult.Add(new Point(lsPtColl[0].X, lsPtColl[0].Y));
                bool lbGetPt = false;
                int lnPivot = lnIntv;

                for (int i = 0; i < lnNData - 1; i++)
                {
                    while (!lbGetPt)
                    {
                        bool lbEndCicle = false;
                        for (int j = (lnPivot - 1); j > i; j--)
                        {
                            double lnDist = DistLine2Point(lsPtColl[i], lsPtColl[lnPivot], lsPtColl[j]);
                            if (lnDist > lnEsp)
                            {
                                lnPivot--;
                                if (lnPivot == (i + 1))
                                {
                                    loResult.Add(lsPtColl[lnPivot]);
                                    lbGetPt = true;
                                }
                                break;
                            }
                            if ((j - 1) == i)
                                lbEndCicle = true;
                        }
                        if (lbEndCicle)
                        {
                            loResult.Add(lsPtColl[lnPivot]);
                            lbGetPt = true;
                        }
                        if (i == (lnPivot - 1))
                            lbGetPt = true;
                    }
                    i = lnPivot - 1;
                    lnPivot += lnIntv;
                    lnPivot = (lnPivot > (lnNData - 1)) ? (lnNData - 1) : lnPivot;
                    lbGetPt = false;
                }
                loResult.Add(new Point(lsPtColl[lnNData - 1].X, lsPtColl[lnNData - 1].Y));
            }
            else
                loResult = lsPtColl;

            return loResult;

        }

        static public PointCollection DouglasPeucker(PointCollection lsPtColl, double lnEsp)
        {
            int lnNData = lsPtColl.Count;
            PointCollection loResult = new PointCollection();

            if (lsPtColl != null && lnNData > 3 && lnEsp > 0)
            {
                List<int> lsIndexKeep = new List<int>();
                lsIndexKeep.Add(0);
                lsIndexKeep.Add(lnNData - 1);

                DouglasReduction(lsPtColl, 0, lnNData - 1, lnEsp, lsIndexKeep);

                lsIndexKeep.Sort();
                foreach (int lnIdx in lsIndexKeep)
                    loResult.Add(lsPtColl[lnIdx]);

                return loResult;
            }
            else
                return loResult;
        }


        private static void DouglasReduction(PointCollection lsPtColl, int lnFstPt, int lnLastPt, double lnEsp, List<int> lsIndKeep)
        {
            double lnMxDist = -1;
            int lnIdxPivot = 0;

            for (int i = lnFstPt + 1; i < lnLastPt; i++)
            {
                double lnDist = DistLine2Point(lsPtColl[lnFstPt], lsPtColl[lnLastPt], lsPtColl[i]);
                if (lnDist > lnMxDist)
                {
                    lnMxDist = lnDist;
                    lnIdxPivot = i;
                }
            }

            if (lnMxDist > lnEsp && lnIdxPivot != 0)
            {
                lsIndKeep.Add(lnIdxPivot);
                DouglasReduction(lsPtColl, lnFstPt, lnIdxPivot, lnEsp, lsIndKeep);
                DouglasReduction(lsPtColl, lnIdxPivot, lnLastPt, lnEsp, lsIndKeep);
            }
        }
        /// <summary>
        /// Gets the distance between a point and a rect.
        /// </summary>
        /// <param name="loLinPt1">Point from rect</param>
        /// <param name="loLinPt2">Point from rect</param>
        /// <param name="loPt">Point of reference</param>
        /// <returns>Distance</returns>
        static public double DistLine2Point(Point loLinPt1, Point loLinPt2, Point loPt)
        {
            double lnDist;
            double lnSlp = (loLinPt2.Y - loLinPt1.Y) / (loLinPt2.X - loLinPt1.X);
            if (double.IsInfinity(lnSlp))
                lnDist = MT.Abs(loLinPt1.X - loPt.X);
            else
            {
                double lnB = loLinPt2.Y - lnSlp * loLinPt2.X;
                lnDist = MT.Abs(lnSlp * loPt.X - loPt.Y + lnB) / MT.Sqrt(lnSlp * lnSlp + 1);
            }
            return lnDist;
        }
    }

    public class Vector2D
    {
        #region Attributes
        private Point goOrig;
        private Point goEnd;
        private Point goCoor;
        private Point goVectUnit;
        private double gnMod;
        #endregion

        #region Properties
        public Point OrgPt
        {
            get { return goOrig; }
            set
            {
                goOrig = value;
                SetValues();
            }
        }

        public Point EndPt
        {
            get { return goEnd; }
            set
            {
                goOrig = value;
                SetValues();
            }
        }

        public Point CoorPt
        {
            get { return goCoor; }
            set
            {
                goEnd = value;
                goOrig = new Point(0, 0);
                SetValues();
            }
        }
        public Point VectUnit
        { get { return goVectUnit; } }
        public double Module
        { get { return gnMod; } }
        #endregion

        #region Constructor

        Vector2D(Point loOrig, Point loEnd)
        {
            goOrig = loOrig;
            goEnd = loEnd;
            SetValues();
        }

        Vector2D(Point loCoor)
        {
            goEnd = loCoor;
            goOrig = new Point(0, 0);
            SetValues();
        }

        #endregion

        #region Methods

        private void SetValues()
        {
            goCoor = new Point((goEnd.X - goOrig.X), (goEnd.Y - goOrig.Y));
            gnMod = Dist2Pts(goOrig, goEnd);
            goVectUnit = new Point(goCoor.X / gnMod, goCoor.Y / gnMod);
        }
        static public double Dist2Pts(Point loPt1, Point loPt2)
        {
            double lnDiffX = (loPt2.X - loPt1.X);
            double lnDiffY = (loPt2.Y - loPt1.Y);
            return (MT.Sqrt(lnDiffX * lnDiffX + lnDiffY * lnDiffY));
        }
        static public double ScalarProdc(Vector2D loVec1, Vector2D loVec2)
        {
            double lnX = loVec1.CoorPt.X * loVec2.CoorPt.X;
            double lnY = loVec1.CoorPt.Y * loVec2.CoorPt.Y;
            return lnX + lnY;
        }
        static public double ModCrossProd(Vector2D loVec1, Vector2D loVec2)
        {
            double lnProd1 = loVec1.CoorPt.X * loVec2.CoorPt.Y;
            double lnProd2 = loVec2.CoorPt.Y * loVec2.CoorPt.X;
            return lnProd1 - lnProd2;
        }
        #endregion
    }
}
