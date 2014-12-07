// Copyright (c) 2012 All Right Reserved, http://www.aworldforus.com

using UnityEngine;
using System.Collections;

public class FlashCompatibleTextReader
{
  private string text_;
  private int index_ = 0;
  private int size_;

	public FlashCompatibleTextReader(string text)
  {
	 text_ = text;
   size_ = text.Length;
	}

  public int Peek()
  {
    if(index_ >= size_)
      return -1;
    return text_[index_];
  }

  public int Read()
  {
    if(index_ >= size_)
      return -1;
    return text_[index_++];
  }
}