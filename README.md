# Star CSharp

## What is this repository for?

STAR is a new programming style developed by Jean‐Jacques Dubray.
He has developed a STAR Component Model (SCM) in two languages

  * Java: https://bitbucket.org/jdubray/star-java 
  * JavaScript: https://bitbucket.org/jdubray/star-javascript 

This STAR library is a custom implementation of the [SAM pattern](http://sam.js.org/).

This repository contains the code of an initial effort to port [the java version (v0.8)](https://bitbucket.org/jdubray/star‐java) of the STAR Component Model (SCM) to C#.

Also one sample ('Factorial') has been ported.
The 'DieHard' sample has been ported but not tested.


### state at june 2017
initial commit. needs a lot of love.

Credits for this initial effort goes to [Nikolay Pasko](https://github.com/nickpasko).


## How do I get set up?
* You can create STAR Component by subclassing the XGen.Star.Scm.Component class.
* You'll need to add one or more behaviors (States, Actions and Type)
* The xgen.io.star.core.simulation allows you to "walk" your component and generate PlantUML activity or State diagrams. For instance, this is the state diagram created during the execution of the Factorial component (xgen.io.star.samples)

## Background

* http://sam.js.org/
* https://www.infoq.com/articles/no-more-mvc-frameworks
* Video explaining the SAM pattern: https://www.youtube.com/watch?v=L_eb7QqtE-I
* Video: Implementing the SAM pattern with angular2: https://www.youtube.com/watch?v=vDZsawI9UNM
* http://www.ebpml.org/blog15/2015/06/sam-the-state-action-model-pattern/
* Background to the apicall example:
http://www.ebpml.org/blog15/2015/06/designing-a-reliable-api-call-component-with-sam/
* https://gitter.im/jdubray/sam

## Some important phrases that helped me understand 

The purpose of an action is to calculate new values (purely function), these values are then presented to a “type” (~store) that decides to accept these values (based on constraints and computes the resulting state). This is semantics I implemented in my framework (STAR – State, Type, Action, Relationship).


The STAR library is just for API orchestration. No front-end.

