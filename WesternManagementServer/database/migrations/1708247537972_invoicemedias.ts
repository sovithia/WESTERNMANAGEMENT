import BaseSchema from '@ioc:Adonis/Lucid/Schema'

export default class Invoicemedias extends BaseSchema {
  protected tableName = 'invoicemedias'

  public async up () {
    this.schema.createTable(this.tableName, (table) => {
      table.increments('id')
      table.timestamps(true)
    })
  }

  public async down () {
    this.schema.dropTable(this.tableName)
  }
}
