﻿using KeyenceSimulation.Interfaces;

namespace KeyenceSimulation.Dto
{
  public class KeyenceMessage : IKeyenceMessage
  {
    public string ToCharStream()
    {
      return @"ST	B0
SE	CC112100	3.52	26
DA	2014/05/13 11:25:49	4F
MS	Std Part Top	E7
LO	A4
SC	0011	6A
CH	94
IT	1	11.048	mm	LN-LN001	11.055	0.200	-0.200	OK	E8
IT	2	2.012	mm	LN-LN002	1.985	0.200	-0.200	OK	8C
IT	3	15.026	mm	LN-LN003	14.970	0.200	-0.200	OK	F5
IT	4	22.397	mm	CL-CL001[CENTER]	22.300	0.200	-0.200	OK	52
IT	5	10.994	mm	CL-CL002[CENTER]	11.013	0.200	-0.200	OK	53
IT	6	135.07	degree(s)	ANGLE001	135.00	2.00	-2.00	OK	E8
IT	7	134.98	degree(s)	ANGLE002	135.00	2.00	-2.00	OK	F3
IT	8	1.998	mm	CORNER001	2.000	0.200	-0.200	OK	FA
IT	9	1.950	mm	CORNER002	2.000	0.200	-0.200	OK	F0
IT	10	1.976	mm	CORNER003	2.000	0.200	-0.200	OK	21
IT	11	1.993	mm	CORNER004	2.000	0.200	-0.200	OK	22
IT	12	2.008	mm	ARC001	2.000	0.200	-0.200	OK	21
IT	13	1.966	mm	ARC002	2.000	0.200	-0.200	OK	2F
IT	14	35.942	mm	DIA001	35.950	0.050	-0.050	OK	A2
EN	9C";
    }
  }
}
