@Browser:chrome
@Smoke
@Regression
Feature: Practice form tests

    Background:
        Given I navigate to practice form page

    Scenario: Successful form fill
        When I fill practice form fields with the following data
          | Label           | Value                   |
          | First Name      | John                    |
          | Last Name       | Doe                     |
          | Email           | john@doe.com            |
          | Gender          | Male                    |
          | Mobile Number   | 1234567891              |
          | Date of Birth   | 11 august 2005          |
          | Subjects        | English, Economics      |
          | Hobbies         | Music                   |
          | Current Address | Unknown, Unknown street |
        When I submit data
        Then thanks message should be visible
        Then I compare the table values with the filled values:
          | Label         | Value                   |
          | Student Name  | John Doe                |
          | Student Email | john@doe.com            |
          | Gender        | Male                    |
          | Mobile        | 1234567891              |
          | Date of Birth | 11 August,2005          |
          | Subjects      | English, Economics      |
          | Hobbies       | Music                   |
          | Address       | Unknown, Unknown street |