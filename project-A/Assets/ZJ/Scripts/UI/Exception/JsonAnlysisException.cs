/***
 * 
 *    Title:不正な形式を解析する
 *   
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
	public class JsonAnlysisException : Exception {
	    public JsonAnlysisException() : base(){}
	    public JsonAnlysisException(string exceptionMessage) : base(exceptionMessage){}
	}
}