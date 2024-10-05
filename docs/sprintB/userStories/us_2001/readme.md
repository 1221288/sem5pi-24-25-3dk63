# US 2001

**Author:** All team members

## 1. Context

This is a new task that hasn't been worked on before.

## 2. Requirements

**US 2001** As Product Owner, I want the system to, continuously, process the files
produced by the Applications Email Bot, so that they can be imported into the system 
by initiative of the Operator
- Priority: 1
- References: See NFR12(SCOMP)

NFR12

The “Applications File Bot” must be developed in C and utilize processes, signals,
pipes, and exec function primitives.

A child process should be created to periodically monitor an input directory for new
files related to the 'Application' phase of the recruitment process. If new files are
detected, a signal should be sent to the parent process.

Please refer to Section 2.2.3 of the “System Specification” document for a
description of the input directory, input files, output directory, and their expected
subdirectories.

Upon receiving a signal, the parent process should distribute the new files among a
fixed number of worker child processes. Each child process will be responsible for
copying all files related to a specific candidate to its designated subdirectory in the
output directory.

Once a child has finished copying all files for a candidate, it should inform its parent
that it is ready to perform additional work. Child workers do not terminate unless they
are specifically terminated by the parent process.

Once all files for all candidates have been copied, the parent process should
generate a report file in the output directory. This report should list, for each
candidate, the name of the output subdirectory and the names of all files that were
copied.

To terminate the application, the parent process must handle the SIGINT signal.
Upon reception, it should terminate all children and wait for their termination.

The names of the input and output directories, the number of worker children, the
time interval for periodic checking of new files, etc., should be configurable. This
configuration can be achieved either through input parameters provided when
running the application or by reading from a configuration file.

Unit and integration tests are highly valued.


## 3. Analysis

_In this section, the team should report the study/analysis/comparison that was done in order to take the best design decisions for the requirement.
This section should also include supporting diagrams/artifacts (such as domain model; use case diagrams, etc.),_

### 3.1. Domain Model

![domain model](out/US2002_DM.svg "Domain Model")

### 3.2. Use case diagram

![use case diagram](out/US2002_UCD.svg "Use case diagram")

## 4. Design

_In this sections, the team should present the solution design that was adopted to solve the requirement.
This should include, at least, a diagram of the realization of the functionality (e.g., sequence diagram),
a class diagram (presenting the classes that support the functionality),
the identification and rational behind the applied design patterns and the specification of the main tests used to validate the functionality._

### 4.1. Realization

### 4.2. Class Diagram

![class diagram](out/US2002_CD.svg "Class Diagram")

### 4.3. System Diagram

![system diagram](out/US2002_SD.svg "System diagram")

### 4.4. Applied Patterns

### 4.5. Tests

**Test 1:** _Verifies that it is not possible to create an instance of the Example class with null values._

```
@Test(expected = IllegalArgumentException.class)
public void ensureNullIsNotAllowed() {
	Example instance = new Example(null, null);
}
```

## 5. Implementation

_In this section the team should present, if necessary, some evidencies that the implementation is according to the design.
It should also describe and explain other important artifacts necessary to fully understand the implementation like, for instance,
configuration files._

_It is also a best practice to include a listing (with a brief summary) of the major commits regarding this requirement._

## 6. Integration/Demonstration

_In this section the team should describe the efforts realized in order to integrate this functionality with the other parts/components of the system_

_It is also important to explain any scripts or instructions required to execute an demonstrate this functionality_

## 7. Observations

_This section should be used to include any content that does not fit any of the previous sections._

_The team should present here, for instance, a critical prespective on the developed work including the analysis of alternative solutioons or related works_

_The team should include in this section statements/references regarding third party works that were used in the development this work._
