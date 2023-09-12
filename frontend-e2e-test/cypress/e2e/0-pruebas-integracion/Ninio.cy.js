describe("Test Ninio", () => {
  //Titulo
  //Antes que nada, abrir el navegador en el proyecto Frontend que es el puerto 8100
  beforeEach(() => {
    cy.visit("http://localhost:8100"); //Frontend de Produccion

    //cambia la el tamaño de la ventana
    cy.viewport(1700,1200);
  });

  //Servicio API - AddUsuario(entidad)
  it("AddUsuario(entidad)", () => {
    cy.get("ion-tab-button").eq(4).click(); // click en el TAB de Usuarios
    cy.wait(1000); //Esperar 1 seg.

    cy.wait(1000); //Esperar 1 seg.

    // inserta el nombre del ninio y lo verifica
    cy.get("#nombreNinio")
      .type("insertar NombreNinio cypress", { delay: 10 })
      .should("have.value", "insertar NombreNinio cypress");

    cy.wait(500); //Esperar medio seg.

    cy.get("#edadNinio")
      .type("4", { delay: 10 })
      .should("have.value", "4");

    cy.wait(500); //Esperar medio seg.
    
  
    // Seleccionar una opción en el ion-select
    cy.get('#sexoNinio')
    .not("[disable]").click();

    // Seleccionar una opción en el ion-select
    cy.get('ion-radio-group.sc-ion-select-popover-md > :nth-child(1)').click();

    //hace clic en en boton agregar
    cy.get("#addNinio").not("[disabled]").click();

    //boton eliminar
    // cy.get(':nth-child(2) > [size="3"] > .ion-color-danger').click();

    //boton editar
    cy.get(':nth-child(2) > [size="3"] > .ion-color-warning').click();

    //cambiar la edad por 5
    cy.get('#ion-input-7')
    .clear()
    .type("5", { delay: 10 })
    .should("have.value", "5");

    //da en guardar cabios
    cy.get('.content-ltr > .ion-color-danger')
    .not("[disable]").click();
  });
});
