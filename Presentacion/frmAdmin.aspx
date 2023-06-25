<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="frmAdmin.aspx.cs" Inherits="Presentacion.frmAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Inicio
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-2">
        <h1 runat="server" id="lblWelcome"></h1>
        <hr />
       <div class="row w-100 mt-2">
                <div class="col-md-3 mt-4">
                    <div class="card border-info mx-sm-1 p-3">
                        <div class="card border-info shadow text-info p-3 my-card d-flex"><span class="fa-solid fa-users" aria-hidden="true"></span></div>
                        <div class="text-info text-center mt-3">
                            <h4>Usuarios</h4>
                        </div>
                        <div class="text-info text-center mt-2">
                            <h1>
                                <asp:Label ID="lblUsuarios" runat="server" Text="Label"></asp:Label></h1>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 mt-4">
                    <div class="card border-success mx-sm-1 p-3">
                        <div class="card border-success shadow text-success p-3 my-card d-flex"><span class="fa-solid fa-user-tie" aria-hidden="true"></span></div>
                        <div class="text-success text-center mt-3">
                            <h4>Lectores</h4>
                        </div>
                        <div class="text-success text-center mt-2">
                            <h1>
                                <asp:Label ID="lblLectores" runat="server" Text="Label"></asp:Label></h1>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 mt-4">
                    <div class="card border-success mx-sm-1 p-3">
                        <div class="card border-success shadow text-success p-3 my-card d-flex"><span class="fa-solid fa-book" aria-hidden="true"></span></div>
                        <div class="text-success text-center mt-3">
                            <h4>Libros</h4>
                        </div>
                        <div class="text-success text-center mt-2">
                            <h1>
                                <asp:Label ID="lblLibros" runat="server" Text="Label"></asp:Label></h1>
                        </div>
                    </div>
                </div>
                 
                <div class="col-md-3 mt-4">
                    <div class="card border-danger mx-sm-1 p-3">
                        <div class="card border-danger shadow text-danger p-3 my-card d-flex"><i class="fa-solid fa-handshake"></i></div>
                        <div class="text-danger text-center mt-3">
                            <h4>Prestamos</h4>
                        </div>
                        <div class="text-danger text-center mt-2">
                            <h1>
                                <asp:Label ID="lblPrestamos" runat="server" Text="Label"></asp:Label></h1>
                        </div>
                    </div>
                </div>
            </div>
       
    </div>
</asp:Content>

