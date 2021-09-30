/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.mycompany.lp_egzaminas;

/**
 *
 * @author vyten
 */
public class Car {
    private String marke;
    private double rida;
    private int metai;
    
    public Car(String marke, double rida, int metai){
        this.marke = marke;
        this.rida = rida;
        this.metai = metai;
    }
    
    public String getMarke(){
        return  marke;
    }
    
    public double getRida(){
        return  rida;
    }
    
    public int getMetai(){
        return  metai;
    }
    
    public void setMarke(String marke){
        this.marke = marke;
    }
    
    public void setRida(double rida){
        this.rida = rida;
    }
    
    public void setMetai(int metai){
        this.metai = metai;
    }

    @Override
    public String toString() {
        return String.format("%14s|%10.2f|%5d", marke, rida, metai);
    }
}
