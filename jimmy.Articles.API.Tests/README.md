## Tests

```SliceFixture``` class contains ```WebApplicationFactory``` initialization and some additional methods which allows to make some database preparation for tests like insert some test data or reset database state to default.

Use ```InitializeAsync()``` to reset database.

Use ```SendAsync()``` to send commands/queries to mock mediator interactions.

