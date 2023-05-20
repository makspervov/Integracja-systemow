package com.lg;
//import com.lg.Sex.*;
import javax.persistence.*;
import java.nio.file.*;
import java.util.List;

public class Main {
    public static void main(String[] args)
    {
        System.out.println("JPA project");
        EntityManagerFactory factory = Persistence.createEntityManagerFactory("Hibernate_JPA");
        EntityManager em = factory.createEntityManager();
        EntityTransaction transaction = em.getTransaction();

        try {
            transaction.begin();

            User u1 = new User("test_1", "test1", "Andrzej", "Kowalski", Sex.MALE);
            em.persist(u1);

            User u2 = new User("test_2", "test2", "Alicja", "Fernandez", Sex.FEMALE);
            em.persist(u2);

            User u3 = new User("test_3", "test3", "Alla", "Maj", Sex.FEMALE);
            em.persist(u3);

            User u4 = new User("test_4", "test4", "Adam", "Kowal", Sex.MALE);
            em.persist(u4);

            User u5 = new User("test_5", "test5", "Agnieszka", "Krawczyk", Sex.FEMALE);
            em.persist(u5);

            //Konwertacja obrazku image.png na tablicę bajtów
            byte[] imageBytes = Files.readAllBytes(Paths.get("src/main/resources/image.png"));

            //Tworzenie nowego użytkownika do tabeli Users o id=6 i dodawanie do niego obrazku
            User u6 = new User("test_6","test6","Maksym","Pervov",Sex.MALE);
            u6.setImage(imageBytes);
            em.persist(u6);


            Role r1 = new Role("Admin");
            em.persist(r1);

            Role r2 = new Role("User");
            em.persist(r2);

            Role r3 = new Role("Moderator");
            em.persist(r3);

            Role r4 = new Role("Guest");
            em.persist(r4);

            Role r5 = new Role("Manager");
            em.persist(r5);


            String jpql = "SELECT u FROM User u WHERE u.sex = :sex";
            List<User> femaleUsers = em.createQuery(jpql, User.class)
                    .setParameter("sex", Sex.FEMALE)
                    .getResultList();
            for (User u : femaleUsers) {
                System.out.println(u.getId() + " " + " " + u.getFirstName() + " " + u.getLastName() + " " + u.getSex());
            }

            // wyszukiwanie użytkownika z id=1 i aktualizacja hasła
            User userToUpdate = em.find(User.class, 1L);
            if (userToUpdate != null) {
                userToUpdate.setPassword("1ts_m3_y0ur_p@sSw0rD");
                em.merge(userToUpdate);
            }

            // wyszukiwanie roli o id=5 i usunięcie
            Role roleToDelete = em.find(Role.class, 5L);
            if (roleToDelete != null) {
                em.remove(roleToDelete);
            }

            transaction.commit();
        } catch (Exception e) {
            transaction.rollback();
            e.printStackTrace();
        } finally {
            em.close();
            em.close();
        }
    }
}
