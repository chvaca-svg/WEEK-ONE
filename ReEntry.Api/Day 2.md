Day 2 – What I did
	•	Created a TODO item entity model (class TodoItem)
	•	Created a Data storage context to hold TODO items in memory
	•	Created the endpoints stated on the requirements with the required content
		•	GET /todos
		•	GET /todos/{id}
		•	POST /todos
		•	DELETE /todos/{id} 

Decisions I made
	•	Used sync API methods for simplicity
	•	Used in-memory EF Core for quick resolution and simplicity
	•	Used minimal API again for simplicity, performance and dev speed

Things that felt rusty
	•	I would have like to implement the APIs endpoint async but I did not know how to do it
	•	On EF core, it took a bit to remember how create the DB context and how to set it to in-memory
	•	I had to google the correct response for POST and DELETE mehtod

Things that felt OK
	•	Writing the API method and writing the entity, also writing the Linq Query

Code
	•	https://github.com/chvaca-svg/WEEK-ONE

What I could do better
	•	Add Openapi documentation
	•	Abstract the data storage context so it can be easily replace with different data source

Questions / Doubts
	•	no questions