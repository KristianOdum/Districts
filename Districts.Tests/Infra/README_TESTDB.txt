Database integration tests

The database integration test currently runs against the local `Districts` database
and uses the existing seed data.

For a real project, I would use a separate test database with predefined seed data. 

This would make the tests independent of the development database and ensure they 
always run against a known state.
