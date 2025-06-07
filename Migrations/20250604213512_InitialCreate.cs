using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuracaoAlerta.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    IdEndereco = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Rua = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Numero = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Bairro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Estado = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.IdEndereco);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    IdFuracao = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Intensidade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.IdFuracao);
                });

            migrationBuilder.CreateTable(
                name: "Abrigos",
                columns: table => new
                {
                    IdAbrigo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CapacidadeTotal = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LotacaoAtual = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Coordenadas = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EnderecoId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abrigos", x => x.IdAbrigo);
                    table.ForeignKey(
                        name: "FK_Abrigos_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "IdEndereco");
                });

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    IdAlerta = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Tipo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    EventoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.IdAlerta);
                    table.ForeignKey(
                        name: "FK_Alertas_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "IdFuracao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    AbrigoId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    EnderecoId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Abrigos_AbrigoId",
                        column: x => x.AbrigoId,
                        principalTable: "Abrigos",
                        principalColumn: "IdAbrigo");
                    table.ForeignKey(
                        name: "FK_Usuarios_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "IdEndereco");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioAlertas",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AlertaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioAlertas", x => new { x.UsuarioId, x.AlertaId });
                    table.ForeignKey(
                        name: "FK_UsuarioAlertas_Alertas_AlertaId",
                        column: x => x.AlertaId,
                        principalTable: "Alertas",
                        principalColumn: "IdAlerta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioAlertas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abrigos_EnderecoId",
                table: "Abrigos",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_EventoId",
                table: "Alertas",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioAlertas_AlertaId",
                table: "UsuarioAlertas",
                column: "AlertaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_AbrigoId",
                table: "Usuarios",
                column: "AbrigoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EnderecoId",
                table: "Usuarios",
                column: "EnderecoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioAlertas");

            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Abrigos");

            migrationBuilder.DropTable(
                name: "Enderecos");
        }
    }
}
