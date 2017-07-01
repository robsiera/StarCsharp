# Star CSharp

## What is this repository for?

STAR is a new programming style developed by Jean‐Jacques Dubray.
He has developed a STAR Component Model (SCM) in two languages

  * Java: https://bitbucket.org/jdubray/star-java 
  * JavaScript: https://bitbucket.org/jdubray/star-javascript 

  This repository contains the code of an initial effort to port [the java version (v0.8)](https://bitbucket.org/jdubray/star‐java) of the STAR Component Model (SCM) to C#.

Also one sample ('Factorial') has been ported.
The 'DieHard' sample has been ported but not tested.

## What is STAR?

STAR is a new programming style developed by Jean‐Jacques Dubray.

This STAR library, containing a STAR Component Model (SCM), is another way 
to implement the [SAM pattern](http://sam.js.org/) 
but in a more "state-machine" way.
It would be very useful in [a full-stack architecture](
http://www.ebpml.org/blog15/2016/08/services-apis-and-microservices-part-2/) for a microservices based backend (below the Model). 

In a Front-End architecture you would end up having to manage too many states 
and it would become cumbersome. There you would implement SAM pattern [as explained on sam.js.org](http://sam.js.org/)



### state at june 2017
initial commit. needs a lot of love and testing

Credits for this initial effort goes to [Nikolay Pasko](https://github.com/nickpasko).


## How do I get set up?
* You can create your own STAR Component by subclassing the XGen.Star.Scm.Component class.
* You'll need to add one or more behaviors (States, Actions and Type)
* The XGen.Star.Scm.Simulation allows you to "walk" your component and generate PlantUML activity or State diagrams. 


## How you can use it

From the Star library you can compute a view from the State.

One way to look at it is the State function outputs a State Representation of the model
that State Representation can be used to compute the view. Because it's at the end of a step, the State Representation is consistent until the next step is executed.
In Angular2 for instance you can use publish/subscribe to pass the State Representation to the View which then renders
The Action/Model/State buckets are very general to organize your code. Any event handler can be structured that way ... and should be.

The way to use the STAR library in a server based front-end architecture is to:
 1. wire requests to actions (via a dispatcher)
 2. then Action -> Model -> State gets executed
 3. you wire the response to the request, to the State's function
 4. Once the response is returned to the browser, 
    the state representation is passed to the view components for rendering.
    You will need to manage the session to rehydrate the user/session's model

    See also [Composition in SAM](http://sam.js.org/#comp)

## Background

* http://sam.js.org/
* https://www.infoq.com/articles/no-more-mvc-frameworks
* Video explaining the SAM pattern: https://www.youtube.com/watch?v=L_eb7QqtE-I
* Video: Implementing the SAM pattern with angular2: https://www.youtube.com/watch?v=vDZsawI9UNM
* http://www.ebpml.org/blog15/2015/06/sam-the-state-action-model-pattern/
* Background to the apicall example:
http://www.ebpml.org/blog15/2015/06/designing-a-reliable-api-call-component-with-sam/
* https://gitter.im/jdubray/sam



## Some quotes

"The STAR library is another way to implement the [SAM pattern](http://sam.js.org/) but in a more "state-machine" way which you may not always want to do. In a Front-End architecture you'd end up having to manage too many states and it would become cumbersome. That's one thing I like a lot about SAM is that there is no discontinuity between a classical state machine semantics and standard coding practices. You can use as many 'states' at it makes sense or as few as necessary."

"The STAR library is just for API orchestration. No front-end. 
SAM is the missing link in Microservice architectures 
because it provides a natural way to achieve consistency. 
The STAR library implementation of SAM is very good at that."

"You can use SAM strictly on the front-end with an API back-end 
or you can actually use [SAM in a full-stack architecture](
http://www.ebpml.org/blog15/2016/08/services-apis-and-microservices-part-2/)"


## Some important quotes that helped me understand SAM 

"SAM is really about breaking the controller in building blocks, decentralize the handler into acceptors on the one hand, and view components on the other. An acceptor being just a unit of work within the Present() method."

"The purpose of an action is to calculate new values (purely function), these values are then presented to a “type” (~store) that decides to accept these values (based on constraints and computes the resulting state). This is semantics I implemented in my framework (STAR – State, Type, Action, Relationship)."

"For state machine semantics SAM works with if-then-else or it works will well defined States like in the STAR library"
