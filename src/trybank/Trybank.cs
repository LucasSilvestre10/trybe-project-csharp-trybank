﻿namespace trybank;

public class Trybank
{
    public bool Logged;
    public int loggedUser;
    
    //0 -> Número da conta
    //1 -> Agência
    //2 -> Senha
    //3 -> Saldo
    public int[,] Bank;
    public int registeredAccounts;
    private int maxAccounts = 50;
    public Trybank()
    {
        loggedUser = -99;
        registeredAccounts = 0;
        Logged = false;
        Bank = new int[maxAccounts, 4];
    }

    // 1. Construa a funcionalidade de cadastrar novas contas
    public void RegisterAccount(int number, int agency, int pass)
    {
        for (int index = 0; index < registeredAccounts; index += 1)
        {
            if (Bank[index, 0] == number && Bank[index, 1] == agency)
                throw new ArgumentException("A conta já está sendo usada!");
        }

        Bank[registeredAccounts, 0] = number;
        Bank[registeredAccounts, 1] = agency;
        Bank[registeredAccounts, 2] = pass;
        Bank[registeredAccounts, 3] = 0;

        registeredAccounts += 1;
    }
    

    // 2. Construa a funcionalidade de fazer Login
    public void Login(int number, int agency, int pass)
    {
        if (Logged)
            throw new AccessViolationException("Usuário já está logado");

        for (int index = 0; index < registeredAccounts; index += 1)
        {
            if (Bank[index, 0] == number && Bank[index, 1] == agency)
            {
                if (Bank[index, 2] != pass)
                    throw new ArgumentException("Senha incorreta");

                Logged = true;
                loggedUser = index;
                return;
            }
        }

        throw new ArgumentException("Agência + Conta não encontrada");
    }

    // 3. Construa a funcionalidade de fazer Logout
    public void Logout()
    {
        if (!Logged)
            throw new AccessViolationException("Usuário não está logado");

        Logged = false;
        loggedUser = -99;
    }

    // 4. Construa a funcionalidade de checar o saldo
    public int CheckBalance()
    {
       if (!Logged)
            throw new AccessViolationException("Usuário não está logado");

        return Bank[loggedUser, 3];
    }

    // 5. Construa a funcionalidade de depositar dinheiro
    public void Deposit(int value)
    {
        if (!Logged)
            throw new AccessViolationException("Usuário não está logado");

        if (value <= 0)
            throw new ArgumentException("Valor inválido");

        Bank[loggedUser, 3] += value;
    }

    // 6. Construa a funcionalidade de sacar dinheiro
    public void Withdraw(int value)
    {
        if (!Logged)
            throw new AccessViolationException("Usuário não está logado");

        if (value <= 0 || value > Bank[loggedUser, 3])
            throw new InvalidOperationException("Saldo insuficiente");

        Bank[loggedUser, 3] -= value;
    }

    // 7. Construa a funcionalidade de transferir dinheiro entre contas
    public void Transfer(int destinationNumber, int destinationAgency, int value)
    {
        if (!Logged)
            throw new AccessViolationException("Usuário não está logado");

        if (value <= 0 || value > Bank[loggedUser, 3])
            throw new InvalidOperationException("Saldo insuficiente");

        for (int index = 0; index < registeredAccounts; index++)
        {
            if (Bank[index, 0] == destinationNumber && Bank[index, 1] == destinationAgency)
            {
                Bank[loggedUser, 3] -= value;
                Bank[index, 3] += value;
                return;
            }
        }
    
    }

   
}
