/**
* @author Jean Denis
*/

public class Person {
    public String firstName;
    public String lastName;
    public int size;
    public String city;

    public Person(String firstName, String lastName, int size, String city) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.size = size;
        this.city = city;
    }

    public void toString(){
    return this.firstName + this.lastName;
    }
}

//just a simple main to test the app
class testapp {
   public static void main (String[] args){
    Person human = new Person( "Jon", "Walker", 180, "Paris");

    System.out.println(Person.toString());

   }
}
