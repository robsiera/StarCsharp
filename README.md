# Star CSharp

## What is this repository for?

STAR is a new programming style developed by Jean‐Jacques Dubray.
He has developed a STAR Component Model (SCM) in two languages
a) Java: https://bitbucket.org/jdubray/star‐java 
b) JavaScript: https://bitbucket.org/jdubray/star‐javascript 

This repository contains the code of an initial effort to port [the java version (v0.8)](https://bitbucket.org/jdubray/star‐java) of the STAR Component Model (SCM) to C#.

Also one sample ('Factorial') has been ported.
The 'DieHard' sample has been ported but not tested.


### state at june 2017
initial commit. needs a lot of love.

Credits for this initial effort goes to [Nikolay Pasko](https://github.com/nickpasko).


## How do I get set up?
* You can create STAR Component by subclassing the xgen.io.star.core.Component class.
* You'll need to add one or more behaviors (States, Actions and Type)
* The xgen.io.star.core.simulation allows you to "walk" your component and generate PlantUML activity or State diagrams. For instance, this is the state diagram created during the execution of the Factorial component (xgen.io.star.samples)

## Background

* http://sam.js.org/
* https://www.infoq.com/articles/no-more-mvc-frameworks
* Video: https://www.youtube.com/watch?v=L_eb7QqtE-I
* http://www.ebpml.org/blog15/2015/06/sam-the-state-action-model-pattern/
* Background to the apicall example:
http://www.ebpml.org/blog15/2015/06/designing-a-reliable-api-call-component-with-sam/
* https://gitter.im/jdubray/sam
