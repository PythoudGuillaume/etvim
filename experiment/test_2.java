/**
* @author Paul Walker
*/

public class Animal{
    public String name;
    public String species;
    public int age;
    public String food;

    public Person(String name, String species, int age, String food) {
        this.name = name;
        this.species = species;
        this.age = age;
        this.food = food;
    }

    public void toString(){
    return this.species + ":" + this.lastName;
    }
}

//just a simple main to test the app
class testapp {
   public static void main (String[] args){
    Person human = new Person( "Bill", "dog", 7, "meat");

    System.out.println(Person.toString());

   }
}
