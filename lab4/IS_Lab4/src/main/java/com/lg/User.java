package com.lg;

import com.sun.istack.NotNull;

import javax.persistence.*;
import java.util.*;

@Entity
@Table(name = "users", indexes = {@Index(name = "idx_login", columnList = "login", unique = true)})
public class User {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    @NotNull
    @Column(nullable = false, unique = true)
    private String login;
    @NotNull
    @Column(nullable = false)
    private String password;
    @NotNull
    @Column(nullable = false)
    private String firstName;
    @NotNull
    @Column(nullable = false)
    private String lastName;
    @Enumerated(EnumType.STRING)
    private Sex sex;

    @OneToMany(fetch = FetchType.EAGER, cascade = CascadeType.ALL)
    private final List<Role> roles = new ArrayList<>();

    @ManyToMany(mappedBy = "users")
    private Set<UsersGroup> groups = new HashSet<>();

    @Lob
    private byte[] image;

    public User(String login, String password, String firstName, String lastName, Sex sex) {
        this.login = login;
        this.password = password;
        this.firstName = firstName;
        this.lastName = lastName;
        this.sex = sex;

    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getLogin() {
        return login;
    }

    public void setLogin(String login) {
        this.login = login;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public Sex getSex() {
        return sex;
    }

    public void setSex(Sex sex) {
        this.sex = sex;
    }

    public List<Role> getRoles() {
        return roles;
    }

    //Zadanie 3

    public void addRole(Role role) {
        roles.add(role);
    }
    public void addGroup(UsersGroup group) {
        groups.add(group);
        group.getUsers().add(this);
    }

    public void removeGroup(UsersGroup group) {
        groups.remove(group);
        group.getUsers().remove(this);
    }

    public byte[] getImage() {
        return image;
    }

    public void setImage(byte[] image) {
        this.image = image;
    }

    //Zadanie 4 i 5

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof User)) return false;
        User user = (User) o;
        return Objects.equals(id, user.id) &&
                Objects.equals(login, user.login) &&
                Objects.equals(password, user.password) &&
                Objects.equals(firstName, user.firstName) &&
                Objects.equals(lastName, user.lastName) &&
                sex == user.sex &&
                Arrays.equals(image, user.image) &&
                Objects.equals(groups, user.groups);
    }

    @Override
    public int hashCode() {
        int result =  Objects.hash(id, login, password, firstName, lastName, sex, groups);
        result = 31*result + Arrays.hashCode(image);
        return result;
    }
}

enum Sex {
    MALE, FEMALE
}