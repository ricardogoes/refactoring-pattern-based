﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternRefactorings.MoveAccumulationVisitor.Better3
{
    //
    // STEP 2 (continued) Extract Method on each other type
    //
    public class TextExtractor
    {
        public List<Node> Nodes;
        private bool _isPreTag = false;
        private bool _isScriptTag = false;
        private readonly StringBuilder _sb = new StringBuilder();

        // Accumulator Method
        public string ExtractText()
        {
            foreach (var node in Nodes)
            {
                if (node is StringNode)
                {
                    AcceptStringNode(node as StringNode);
                }
                else if (node is LinkTag)
                {
                    AcceptLinkTag(node as LinkTag);
                }
                else if (node is EndTag)
                {
                    AcceptEndTag(node as EndTag);
                }
                else if (node is Tag)
                {
                    AcceptTag(node as Tag);
                }
            }
            return _sb.ToString();
        }
  
        private void AcceptTag(Tag tag)
        {
            string tagName = tag.GetTagName();
            if (tagName.ToUpper() == "PRE")
            {
                _isPreTag = true;
            }
            else if (tagName.ToUpper() == "SCRIPT")
            {
                _isScriptTag = true;
            }
        }
  
        private void AcceptEndTag(EndTag endTag)
        {
            string tagName = endTag.GetTagName();
            if (tagName.ToUpper() == "PRE")
            {
                _isPreTag = false;
            }
            else if (tagName.ToUpper() == "SCRIPT")
            {
                _isScriptTag = false;
            }
        }
  
        private void AcceptLinkTag(LinkTag linkTag)
        {
            if (_isPreTag)
            {
                _sb.Append(linkTag.GetLinkText());
            }
            if (GetLinks())
            {
                _sb.Append("<");
                _sb.Append(linkTag.GetLink());
                _sb.Append(">");
            }
        }

        private void AcceptStringNode(StringNode stringNode)
        {
            if (!_isScriptTag)
            {
                if (_isPreTag)
                {
                    _sb.Append(stringNode.GetText());
                }
                else
                {
                    // details omitted
                }
            }
        }
  
        private bool GetLinks()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }

    public class Node
    {
    }

    public class StringNode : Node
    {
        public string GetText()
        {
            throw new NotImplementedException();
        }
    }

    public class LinkTag : Node
    {
        public string GetLink()
        {
            throw new NotImplementedException();
        }

        public string GetLinkText()
        {
            throw new NotImplementedException();
        }
    }

    public class EndTag : Node
    {
        public string GetTagName()
        {
            throw new NotImplementedException();
        }
    }

    public class Tag : Node
    {
        public string GetTagName()
        {
            throw new NotImplementedException();
        }
    }

}
