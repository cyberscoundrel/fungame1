using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticking
{

	public bool tickActive;

    public Thread tThread;

    //public abstract void tickProc();

    public virtual void tickThread(object tickProc)
    {
    	while(tickActive)
    	{
    		((Action)tickProc)();

    	}
    }

    public virtual void initTick(Action tickProc)
    {
    	tickActive = true;
    	tThread = new Thread(tickThread);
    	tThread.Start(tickProc);
    }

    public virtual void restartTick()
	{
		tickActive = true;
		tThread.Start();

	}

	public virtual void ceaseTick()
	{
		tickActive = false;
	}

}
